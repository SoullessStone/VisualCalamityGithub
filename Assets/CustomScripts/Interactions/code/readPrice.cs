using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readPrice : MonoBehaviour
{

    public int price = 0;
    int priceDelta = 0;
    int maxSteps = 30;
    int steps = 0;
    bool isNewPriceActive = false;
    int numOfPrices = 0;
    public TextMesh text;
    public GameObject collection;

    // Use this for initialization
    void Start()
    {
        text.text = "CHF " + string.Format("{0:N0}", 0) + ".-";

    }

    // Update is called once per frame
    void Update()
    {
        {
            if (!isNewPriceActive)
            {
                if (isNewPrice())
                {
                    isNewPriceActive = true;
                    steps = 0;
                }
            }
            else
            {
                if (steps > maxSteps)
                {
                    text.text = "CHF " + string.Format("{0:N0}", calculatePrice()) + ".-";
                    isNewPriceActive = false;
                    steps = 0;
                }
                steps++;
            }
        }
    }

    int calculatePrice()
    {
        int newPrice = 0;

        foreach (Transform child in collection.transform)
        {
            PriceController childPrice = child.GetComponent<PriceController>();
            if (childPrice != null)
            {
                newPrice += childPrice.Price;
            }
        }

        return newPrice;
    }

    bool isNewPrice()
    {
        int newNumOfPrices = 0;

        foreach (Transform child in collection.transform)
        {
            PriceController childPrice = child.GetComponent<PriceController>();
            if (childPrice != null)
            {
                newNumOfPrices++;
            }
        }

        if (newNumOfPrices > numOfPrices)
        {
            numOfPrices = newNumOfPrices;
            return true;
        }
        else
        {
            return false;
        }
    }

}
