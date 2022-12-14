// by ALEXANDRE Dorian
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.Events;

public class GoldManager : MonoBehaviour
{
    [SerializeField]
    private int _startFortune;

    public int _currentFortune;

    [SerializeField]
    private TextMeshProUGUI _currentMoneyText;

    [SerializeField]
    private List<PurchaseHistory> _purchaseHistory = new List<PurchaseHistory>();


    string filename = "";
    public UnityEvent FortuneChanged;

    private void Start()
    {
        _currentFortune = _startFortune;
        SetGoldDisplayValue();

        string date = System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString() + "_" + System.DateTime.Now.Hour.ToString() + "-" + System.DateTime.Now.Minute.ToString();

        filename = Application.dataPath + "/EconomyDatas" + "/purchaseHistory_" + date + ".csv";
        
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
        FortuneChanged.Invoke();
        SetGoldDisplayValue();
    }

    public void CollectMoney(int money) //Obtain money
    {
        _currentFortune += money;
        FortuneChanged.Invoke();
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
        purchaseHistory.waveIndex = LevelReferences.Instance.SpawnerManager.getCurrentWave;

        return purchaseHistory;
    }

    [ContextMenu("ExportPurchaseToCSV")]
    public void ExportPurchaseToCSV()
    {
        
        if (_purchaseHistory.Count > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Name, Price, Previous Fortune, Wave Number, Time"); 
            tw.Close();

            tw = new StreamWriter(filename, true);

            for(int i = 0; i < _purchaseHistory.Count; i++)
            {
                tw.WriteLine(_purchaseHistory[i].name + "," + _purchaseHistory[i].price + "," + _purchaseHistory[i].previousFortune + "," + _purchaseHistory[i].waveIndex + "," + _purchaseHistory[i].time);
            }
            tw.Close();

            Debug.Log("Data save to : " + Application.dataPath);
        }
    }



    [System.Serializable]
    public class PurchaseHistory
    {
        public int price;
        public string name;
        public float time;
        public float previousFortune;
        public int waveIndex;

    }
}
