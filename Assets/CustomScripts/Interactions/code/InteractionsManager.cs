using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class InteractionsManager : MonoBehaviour, IInputClickHandler
{
    public GameObject pointofInterest;
    private Firescript firescript;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Instantiate(pointofInterest, firescript.hitPoint, Quaternion.Euler(firescript.normalHit.x, firescript.normalHit.y, firescript.normalHit.z));
    }

	// Use this for initialization
	void Start () {
        firescript = FindObjectOfType<Firescript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
