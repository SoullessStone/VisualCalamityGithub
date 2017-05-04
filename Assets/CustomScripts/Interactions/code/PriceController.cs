using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceController : MonoBehaviour {
    public int Price = 1000;
    private ClientManager clientManager;

    // Use this for initialization
    void Start () {
        GameObject PriceCollection = GameObject.Find("HologramCollection");
        this.transform.SetParent(PriceCollection.transform);


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
