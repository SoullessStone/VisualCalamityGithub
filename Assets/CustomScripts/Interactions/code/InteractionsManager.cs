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
    private CameraManager cameraManager;
    private RaycastCollisions raycastCollisionsScript;
    private ImageDemo imageDemo;
    private ReadSchadenPrice readSchadenPrice;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //Instantiate(pointofInterest, firescript.hitPoint, Quaternion.Euler(firescript.normalHit.x, firescript.normalHit.y, firescript.normalHit.z));
    }

	// Use this for initialization
	void Start () {
        raycastCollissions = FindObjectOfType<RaycastCollisions>();
        cameraManager = FindObjectOfType<CameraManager>();
        raycastCollisionsScript = FindObjectOfType<RaycastCollisions>();
        imageDemo = FindObjectOfType<ImageDemo>();
        readSchadenPrice = FindObjectOfType<ReadSchadenPrice>();
        readSchadenPrice.disablePriceView();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSpeechKeywordRecognized(SpeechKeywordRecognizedEventData eventData)
    {
        switch (eventData.RecognizedText)
        {
            case "Set Value":
                SetPrice();
                break;
            case "Fire Start":
                flareMobile.SetActive(true);
                readSchadenPrice.enablePriceView();
                break;
            case "Fire Stop":
                flareMobile.SetActive(false);
                readSchadenPrice.disablePriceView();
                break;
            case "Load Snap":
                LoadSnap();
                break;
            case "Take Snap":
                cameraManager.takePhoto();
                break;
        }

    }

    private IEnumerator LoadSnap()
    {
        if (raycastCollisionsScript.collision != null)
        {
            cameraManager.setFocus(raycastCollisionsScript.collision);
            WWW www = new WWW("https://holodemomobi.blob.core.windows.net/image/" + raycastCollisionsScript.collision.name);
            yield return www;
            raycastCollisionsScript.collision.GetComponent<Renderer>().material.mainTexture = www.texture;
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

        cameraManager.setFocus(lastCreated);
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
