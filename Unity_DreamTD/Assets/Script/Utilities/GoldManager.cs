// by ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    [SerializeField]
    private int _startFortune;

    [SerializeField]
    private int _currentFortune;

    [SerializeField]
    private TextMeshProUGUI _currentMoneyText;

    [SerializeField]
    private List<PurchaseHistory> _purchaseHistory = new List<PurchaseHistory>();

    private void Start()
    {
        _currentFortune = _startFortune;
        SetGoldDisplayValue();
    }

    public int getFortune // Return current fortune
    {
        get { return _currentFortune; }
    }

    public bool CanBuy(int price) //Test if player can buy something
    {
        return _currentFortune >= price;
    }

    public void Buy(int price, string ObjectName) //After a purchase, money is decreased
    {
        _currentFortune -= price;

        _purchaseHistory.Add(CreatePurchase(price, ObjectName));
        SetGoldDisplayValue();
    }

    public void CollectMoney(int money) //Obtain money
    {
        _currentFortune += money;
        SetGoldDisplayValue();
    }

    public void SetGoldDisplayValue() //Set current fortune on player's display
    {
        _currentMoneyText.text = _currentFortune.ToString();
    }

    public PurchaseHistory CreatePurchase(int price, string name) //Create a Purchase History to conserv every game data
    {
        PurchaseHistory purchaseHistory = new PurchaseHistory();

        purchaseHistory.price = price;
        purchaseHistory.name = name;
        purchaseHistory.time = Time.time;
        purchaseHistory.previousFortune = _currentFortune + price;

        return purchaseHistory;
    }



    [System.Serializable]
    public class PurchaseHistory
    {
        public int price;
        public string name;
        public float time;
        public float previousFortune;

    }
}
