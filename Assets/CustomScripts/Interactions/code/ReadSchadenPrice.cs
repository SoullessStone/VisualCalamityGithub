using UnityEngine;

public class ReadSchadenPrice : MonoBehaviour
{
    private int removedItems = 0;
    public int price = 0;
    int priceDelta = 0;
    public TextMesh text;
    public int insuredValuePerObject = 900; 

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int targetPrice = calculatePrice();
        priceDelta = targetPrice - price;
        if (priceDelta != 0)
        {
            if (priceDelta > 0)
            {
                int delta = System.Math.Min(priceDelta, 97);
                priceDelta -= delta;
                price += delta;
            }
            else
            {
                int delta = System.Math.Max(priceDelta, -97);
                priceDelta += delta;
                price -= delta;
            }
        }
        text.text = "CHF " + string.Format("{0:N0}", price) + ".-";
    }

    private int calculatePrice()
    {
        return insuredValuePerObject * removedItems;
    }

    public void increaseRemovedItems()
    {
        removedItems++;
    }

    public void enablePriceView()
    {
        this.gameObject.SetActive(true);
    }

    public void disablePriceView()
    {
        this.gameObject.SetActive(false);
    }
}