using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readPrice : MonoBehaviour {

    public int price = 0;
    int priceDelta = 0;
    public TextMesh text;
    public GameObject collection;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        int newPrice = calculatePrice();
        priceDelta = newPrice - price;
        if (priceDelta != 0)
        {
            if (priceDelta > 0)
            {
                int delta = System.Math.Min(priceDelta, 97);
                priceDelta -= delta;
                price += delta;
            } else
            {
                int delta = System.Math.Max(priceDelta, -97);
                priceDelta += delta;
                price -= delta;
            }
        }
        text.text = "CHF " + string.Format("{0:N0}", price) + ".-";
    }

    int calculatePrice()
    {
        int newPrice = 0;

        foreach (Transform child in collection.transform)
        {
            PriceController childPrice = child.GetComponent<PriceController>();
            if(childPrice != null)
            {
                newPrice += childPrice.Price;
            }
        }

        return newPrice;
    }
}
