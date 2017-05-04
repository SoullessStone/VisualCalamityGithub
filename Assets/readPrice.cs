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
                int delta = System.Math.Min(priceDelta, 13);
                priceDelta -= delta;
                price += delta;
            } else
            {
                int delta = System.Math.Max(priceDelta, -13);
                priceDelta += delta;
                price -= delta;
            }
        }
        text.text = price.ToString();
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
