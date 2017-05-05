using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class PriceController : MonoBehaviour,ISpeechHandler
{
    public int Price = 1000;
    private ClientManager clientManager;
    private RaycastCollisions raycastCollisionsScript;
    private ImageDemo imageDemo;
    private CameraManager cameraManager;

    public void OnSpeechKeywordRecognized(SpeechKeywordRecognizedEventData eventData)
    {
        switch (eventData.RecognizedText)
        {

            case "Load Snap":
                StartCoroutine(LoadSnap());
                break;
        }
    }

    private IEnumerator LoadSnap()
    {
        if (raycastCollisionsScript.collision != null)
        {
            cameraManager.setFocus(raycastCollisionsScript.collision);
            WWW www = new WWW("https://holodemomobi.blob.core.windows.net/image/" + raycastCollisionsScript.collision.name + ".jpg");
            yield return www;
            raycastCollisionsScript.collision.GetComponent<Renderer>().material.mainTexture = www.texture;
        }

    }

    // Use this for initialization
    void Start () {
        raycastCollisionsScript = FindObjectOfType<RaycastCollisions>();
        imageDemo = FindObjectOfType<ImageDemo>();
        cameraManager = FindObjectOfType<CameraManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
