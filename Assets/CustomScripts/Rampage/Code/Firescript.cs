using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firescript : MonoBehaviour {

    public GameObject obj;
    public LayerMask[] RaycastLayerMasks = new LayerMask[] { Physics.DefaultRaycastLayers };
    public Vector3 normalHit;
    public Vector3 hitPoint;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit = new RaycastHit();
        //int layerMask = 1 << 8;
        
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit)
            //&& !hit.transform.gameObject.GetType().Equals(obj.GetType())
            )
        {
            normalHit = hit.normal;
            hitPoint = hit.point;

            obj.transform.position = hit.point;
            
            obj.SetActive(true);
        }
        else
        {
            normalHit = Vector3.zero;
            hitPoint = Vector3.zero;
            obj.SetActive(false);
        }
	}
}
