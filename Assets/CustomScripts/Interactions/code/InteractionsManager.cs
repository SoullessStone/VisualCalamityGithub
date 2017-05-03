using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class InteractionsManager : MonoBehaviour, IInputClickHandler
{
    public GameObject pointofInterest;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("hey");
        Instantiate(pointofInterest);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
