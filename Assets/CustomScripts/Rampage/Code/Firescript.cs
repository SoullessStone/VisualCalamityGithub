using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firescript : MonoBehaviour {

    public GameObject obj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit = new RaycastHit();
        
		if(Physics.Raycast(transform.position, Camera.main.transform.forward, out hit))
        {
            obj.transform.position = hit.point;
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
	}
}
