using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeAndCollide : MonoBehaviour {

    private ReadSchadenPrice readSchadenPrice;

    // Use this for initialization
    void Start () {
        readSchadenPrice = FindObjectOfType<ReadSchadenPrice>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PointOfInterest"))
        {
            other.gameObject.SetActive(false);
            readSchadenPrice.increaseRemovedItems();
        }
    }

    public void activateObject()
    {
        this.gameObject.SetActive(true);
    }

    public void deactivateObject()
    {
        this.gameObject.SetActive(false);
    }
}
