using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using System;
using HoloToolkit.Unity;

public class InteractionsManager : MonoBehaviour, IInputClickHandler, ISpeechHandler
{
    
    public GameObject pointofInterest;
    private RaycastCollisions raycastCollissions;
    public GameObject flareMobile;
    private VisualizeAndCollide visualizeAndCollide;
    private CameraManager cameraManager;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //Instantiate(pointofInterest, firescript.hitPoint, Quaternion.Euler(firescript.normalHit.x, firescript.normalHit.y, firescript.normalHit.z));
    }

	// Use this for initialization
	void Start () {
        raycastCollissions = FindObjectOfType<RaycastCollisions>();
        visualizeAndCollide = flareMobile.GetComponent<VisualizeAndCollide>();
        cameraManager = FindObjectOfType<CameraManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSpeechKeywordRecognized(SpeechKeywordRecognizedEventData eventData)
    {
        switch (eventData.RecognizedText)
        {
            case "Set Price":
                SetPrice();
                break;
            case "Remove":
                Remove();
                break;
            case "Fire Start":
                flareMobile.SetActive(true);
                break;
            case "Fire Stop":
                flareMobile.SetActive(false);
                break;
            case "Room Save":
                RoomSave();
                break;
            case "Room Import":
                RoomImport();
                break;
            case "Burn":
                // Todo: Call FireScript
                break;
            case "Snapshot":
                cameraManager.takePhoto();
                break;
        }

    }

    private void SetPrice()
    {
        Quaternion toQuat = Camera.main.transform.localRotation;
        toQuat.x = 0;
        toQuat.z = 0;
        GameObject lastCreated = Instantiate(pointofInterest, raycastCollissions.hitPoint - gameObject.transform.position, toQuat, GameObject.Find("HologramCollection").transform);

        //Anchor Stuff
        ClientManager clientManager = GameObject.Find("ClientManager").GetComponent<ClientManager>();
        clientManager.AnchorCounter++;
        string tmp = String.Concat(clientManager.ClientId, clientManager.AnchorCounter.ToString());
        WorldAnchorManager.Instance.AttachAnchor(lastCreated, tmp);

        cameraManager.setLastCreated(lastCreated);
    }

    private void Remove()
    {
    }

    private void RoomSave()
    {

    }

    private void RoomImport()
    {

    }
}
