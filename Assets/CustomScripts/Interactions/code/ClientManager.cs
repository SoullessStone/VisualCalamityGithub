using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA;

public class ClientManager : MonoBehaviour
{

    public string ClientId = "18236782";
    public int AnchorCounter = 0;
    public GameObject PointOfInterest;
    public GameObject Danger;
    public GameObject Remark;


    // Use this for initialization
    void Start()
    {
        Invoke("importAnchors", 2);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void importAnchors()
    {
        if (WorldAnchorManager.Instance.AnchorStore != null)
        {

            String[] savedAnchorList = WorldAnchorManager.Instance.AnchorStore.GetAllIds();

            foreach (string s in savedAnchorList)
            {
                if (s.StartsWith(ClientId))
                {
                   //first is client number, second type, third photo number
                    String [] splittedAnchor = s.Split('_');

                    int number = Int32.Parse(splittedAnchor[2]);
                    if (AnchorCounter < number)
                    {
                        AnchorCounter = number;
                    }
                    GameObject go;
                    switch (splittedAnchor[1])
                    {
                        case "DAN":
                            go = Danger;
                            break;
                        case "POI":
                            go = PointOfInterest;
                            break;
                        default:
                            go = Remark;
                            break;
                    }

                    GameObject obj = Instantiate(go, GameObject.Find("HologramCollection").transform);
                    //set name to client
                    obj.name = s;
                    WorldAnchor world = WorldAnchorManager.Instance.AnchorStore.Load(s, obj);

                    world.OnTrackingChanged += World_OnTrackingChanged;
               
                    //WorldAnchorManager.Instance.AttachAnchor(Hologram, s);
                }
            }
        }
        else
        {
            Invoke("importAnchors", 5f);
        }
    }

    private void World_OnTrackingChanged(WorldAnchor self, bool located)
    {
        // This simply activates/deactivates this object and all children when tracking changes
        self.gameObject.SetActive(located);
    }
}
