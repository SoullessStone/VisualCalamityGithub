using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using System;
using HoloToolkit.Unity;

public class InteractionsManager : MonoBehaviour, IInputClickHandler, ISpeechHandler
{
    
    public GameObject pointofInterest;
	public GameObject danger;
	public GameObject remark;
	public GameObject flareMobile;

	private RaycastCollisions raycastCollissions;
    private CameraManager cameraManager;
    private ClientManager clientManager;

    private ImageDemo imageDemo;
    private ReadSchadenPrice readSchadenPrice;
	private Transform hologramCollection;


    public void OnInputClicked(InputClickedEventData eventData)
    {
        //Instantiate(pointofInterest, firescript.hitPoint, Quaternion.Euler(firescript.normalHit.x, firescript.normalHit.y, firescript.normalHit.z));
    }

	// Use this for initialization
	void Start () {
        raycastCollissions = FindObjectOfType<RaycastCollisions>();
        cameraManager = FindObjectOfType<CameraManager>();
        
        imageDemo = FindObjectOfType<ImageDemo>();
        readSchadenPrice = FindObjectOfType<ReadSchadenPrice>();
        readSchadenPrice.disablePriceView();

		hologramCollection = GameObject.Find ("HologramCollection").transform;
        clientManager = GameObject.Find("ClientManager").GetComponent<ClientManager>();
    }
	
    public void OnSpeechKeywordRecognized(SpeechKeywordRecognizedEventData eventData)
    {
        switch (eventData.RecognizedText)
        {
            case "Set Value":
				SetRelevantPoint(pointofInterest, "POI");
                break;
			case "Set Danger":
				SetRelevantPoint(danger, "DAN");
				break;
			case "Mark Object":
				SetRelevantPoint(remark, "REM");
				break;
			case "Take Picture":
				cameraManager.takePhoto();
				break;
            case "Fire Start":
                flareMobile.SetActive(true);
                readSchadenPrice.enablePriceView();
                break;
            case "Fire Stop":
                flareMobile.SetActive(false);
                readSchadenPrice.disablePriceView();
                break;
            /*case "Set Focus":            
                cameraManager.setFocus(raycastCollissions.lastCollide);
                break;*/
        }

    }

	private void SetRelevantPoint (GameObject relevantObject, String objectType)
	{
		Quaternion toQuat = Camera.main.transform.localRotation;
		toQuat.x = 0;
		toQuat.z = 0;
		GameObject lastCreated = Instantiate(relevantObject, raycastCollissions.hitPoint - gameObject.transform.position, toQuat, hologramCollection);

        lastCreated.GetComponent<RotationOnY>().rotationSpeed = 100;

        //Anchor Stuff		
        clientManager.AnchorCounter++;
		string tmp = String.Concat(clientManager.ClientId, "_" + objectType);
		tmp = String.Concat(tmp, "_" + clientManager.AnchorCounter.ToString());
        lastCreated.name = tmp;
		WorldAnchorManager.Instance.AttachAnchor(lastCreated, tmp);

        cameraManager.lastCreated = lastCreated;
        cameraManager.takePhoto();
        cameraManager.setFocus(lastCreated);
	}
		
}
