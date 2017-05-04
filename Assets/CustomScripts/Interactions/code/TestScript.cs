using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Persistence;

public class TestScript : MonoBehaviour {

    public WorldAnchorStore AnchorStore { get; private set; }
    public GameObject GameObject;
    public string Client = "test";  
    // Use this for initialization
    void Start () {
   
        WorldAnchorStore.GetAsync(AnchorStoreReady);

   
    }

    private void AnchorStoreReady(WorldAnchorStore store)
    {
        AnchorStore = store;
        LoadGame();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void LoadGame()
    {

        // Save data about holograms positioned by this world anchor
        AnchorStore.Load("test", gameObject);

        string[] ids = AnchorStore.GetAllIds();
        for (int index = 0; index < ids.Length; index++)
        {
            Debug.Log(ids[index]);
        }
    }
}
