using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollisions : MonoBehaviour {

    public Vector3 hitPoint;
    public GameObject collision;
    private GameObject lastCollide;

    private CameraManager cameraManager;

    // Use this for initialization
    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit))
        {
            OnHit(hit);
        }
        else
        {
            hitPoint = Vector3.zero;
            collision = null;
        }
    }

    private void OnHit(RaycastHit hit)
    {
        hitPoint = hit.point;

        collision = null;

        if (isRelevantObject(ref hit))
        {
            collision = hit.transform.gameObject;
            
            if(lastCollide == null)
            {
                lastCollide = collision;
            }

            if(cameraManager.lastCreated != collision)
            {
                cameraManager.setFocus(collision);
                lastCollide = collision;                
            }               
        }
    }

    private static bool isRelevantObject(ref RaycastHit hit)
    {
        return hit.transform.gameObject != null && 
                    (hit.transform.gameObject.CompareTag("PointOfInterest")
                    || hit.transform.gameObject.CompareTag("PointOfDanger")
                    || hit.transform.gameObject.CompareTag("Remark"));
    }
}
