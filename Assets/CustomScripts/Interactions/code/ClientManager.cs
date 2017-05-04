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
    public GameObject Hologram;


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
                    string tmp = s.Remove(0, 8);
                    Debug.Log(tmp);
                    int number = Int32.Parse(tmp);
                    if (AnchorCounter < number)
                    {
                        AnchorCounter = number;
                    }
                    WorldAnchor world = WorldAnchorManager.Instance.AnchorStore.Load(s, Hologram);

                    world.OnTrackingChanged += World_OnTrackingChanged;
               
                    GameObject go= GameObject.CreatePrimitive(PrimitiveType.Cube);
                    go.transform.position = world.transform.position;
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
