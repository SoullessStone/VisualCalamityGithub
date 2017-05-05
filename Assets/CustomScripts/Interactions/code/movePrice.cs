using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePrice : MonoBehaviour
{
    int price = 1000;
    int numOfPrices = 0;
    int maxSteps = 30;
    int steps = 0;
    float xPosEnd = 0.526f;
    float yPosEnd = -0.25f;
    Vector3 startPos;
    bool isNewPriceActive = false;
    public TextMesh text;
    public GameObject collection;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
            text.transform.Translate(xPosEnd / (float)maxSteps, yPosEnd / (float)maxSteps, 0);
            text.text = "CHF " + string.Format("{0:N0}", price) + ".-";
            steps++;
            if (steps > maxSteps)
            {
                isNewPriceActive = false;
                steps = 0;
                text.transform.Translate(-xPosEnd, -yPosEnd, 0);
                text.text = "";
            }
        }
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
