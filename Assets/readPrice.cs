using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readPrice : MonoBehaviour {

    public int price = 1000;
    public TextMesh text;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        price++;
        if(price > 2000000)
        {
            price = 1000;
        }
        text.text = price.ToString();
    }
}
