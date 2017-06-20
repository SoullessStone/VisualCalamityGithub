using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeAndCollide : MonoBehaviour {

    private ReadSchadenPrice readSchadenPrice;
    public GameObject fireParticle;

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
            Instantiate(fireParticle,new Vector3(other.gameObject.transform.position.x, 
                other.gameObject.transform.position.y, other.gameObject.transform.position.z),Quaternion.identity);
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
