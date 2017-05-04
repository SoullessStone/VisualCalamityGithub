using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeAndCollide : MonoBehaviour {

    public bool rampageModeActive;

    // Use this for initialization
    void Start () {
        rampageModeActive = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (rampageModeActive)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (rampageModeActive && other.gameObject.CompareTag("PointOfInterest"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
