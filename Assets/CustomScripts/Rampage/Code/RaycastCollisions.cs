using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollisions : MonoBehaviour {

    public Vector3 normalHit;
    public Vector3 hitPoint;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit))
        {
            normalHit = hit.normal;
            hitPoint = hit.point;
        }
        else
        {
            normalHit = Vector3.zero;
            hitPoint = Vector3.zero;
        }
    }

}
