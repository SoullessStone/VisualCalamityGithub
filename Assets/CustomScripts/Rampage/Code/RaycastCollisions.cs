using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollisions : MonoBehaviour {

    public Vector3 normalHit;
    public Vector3 hitPoint;
    public GameObject collision;
    public GameObject lastCollide;
    public CameraManager cameraManager;

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
            normalHit = hit.normal;
            hitPoint = hit.point;
           
            if (hit.transform.gameObject != null && (hit.transform.gameObject.CompareTag("PointOfInterest")||
                hit.transform.gameObject.CompareTag("PointOfDanger")))
            {
                collision = hit.transform.gameObject;
                if (lastCollide != collision)
                {
                    cameraManager.setFocus(collision);
                    cameraManager.image.SetActive(false);
                    if(lastCollide!=null)
                        lastCollide.SetActive(true);
                                      
                }
                lastCollide = collision;
            }
            else
            {
                collision = null;
            }
        }
        else
        {
            normalHit = Vector3.zero;
            hitPoint = Vector3.zero;
            collision = null;
        }
    }



}
