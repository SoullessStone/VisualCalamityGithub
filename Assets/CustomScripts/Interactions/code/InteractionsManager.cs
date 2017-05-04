using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class InteractionsManager : MonoBehaviour, IInputClickHandler, ISpeechHandler
{
    
    public GameObject pointofInterest;
    private Firescript firescript;


    public void OnInputClicked(InputClickedEventData eventData)
    {
        //Instantiate(pointofInterest, firescript.hitPoint, Quaternion.Euler(firescript.normalHit.x, firescript.normalHit.y, firescript.normalHit.z));
    }

	// Use this for initialization
	void Start () {
        firescript = FindObjectOfType<Firescript>();
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
            case "Rampage Start":
                // Todo: Call FireScript
                break;
            case "Rampage Stop":
                // Todo: Call FireScript
                break;
            case "Room Save":
                RoomSave();
                break;
            case "Room Import":
                RoomImport();
                break;
            case "Dracarys":
                // Todo: Call FireScript
                break;
        }

    }

    private void SetPrice()
    {
        Quaternion toQuat = Camera.main.transform.localRotation;
        toQuat.x = 0;
        toQuat.z = 0;
        Instantiate(pointofInterest, firescript.hitPoint - gameObject.transform.position, toQuat);
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
