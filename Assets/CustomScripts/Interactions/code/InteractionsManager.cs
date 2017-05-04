using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class InteractionsManager : MonoBehaviour, IInputClickHandler, ISpeechHandler
{
    
    public GameObject pointofInterest;
    private RaycastCollisions raycastCollissions;
    private VisualizeAndCollide visualizeAndCollide;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //Instantiate(pointofInterest, firescript.hitPoint, Quaternion.Euler(firescript.normalHit.x, firescript.normalHit.y, firescript.normalHit.z));
    }

	// Use this for initialization
	void Start () {
        raycastCollissions = FindObjectOfType<RaycastCollisions>();
        visualizeAndCollide = FindObjectOfType<VisualizeAndCollide>();
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
                visualizeAndCollide.rampageModeActive = true;
                break;
            case "Fire Stop":
                visualizeAndCollide.rampageModeActive = false;
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
        }

    }

    private void SetPrice()
    {
        Quaternion toQuat = Camera.main.transform.localRotation;
        toQuat.x = 0;
        toQuat.z = 0;
        Instantiate(pointofInterest, raycastCollissions.hitPoint - gameObject.transform.position, toQuat);
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
