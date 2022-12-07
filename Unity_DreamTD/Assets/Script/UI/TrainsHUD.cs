using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy, modified by ALBERT Esteban to update stats via S.O
public class TrainsHUD : MonoBehaviour
{
    public GameObject train;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject wagonsHUD;
    [SerializeField] private GameObject changeTypeHUD;
    [SerializeField] private GameObject upgradeButton;
    private TrainLevel trainLevel;
    private Locomotive _pickedLocomotive = null;

    //HUD info
    public void PickTrain(GameObject pickedTrain)
    {
        train = pickedTrain;
        _pickedLocomotive = train.GetComponentInChildren<Locomotive>();
        trainLevel = train.transform.GetComponentInChildren<TrainLevel>();
        //text.SetText("Level " + trainLevel.currentLevel);
        text.SetText("Level " + _pickedLocomotive.CurrentSpeedLevel);
        for (var i = 0; i != _pickedLocomotive.wagons.Count; i++)
        {
            if (_pickedLocomotive.wagons[i].gameObject.GetComponent<MeshRenderer>().enabled == true)
            {
                wagonsHUD.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                wagonsHUD.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        //if (trainLevel.currentLevel >= trainLevel.maxLevel)
        //{
        //    upgradeButton.SetActive(false);
        //}
        if (_pickedLocomotive.CurrentWagonCountLevel >= _pickedLocomotive.TrainStats.MaxWagonCount)
        {
            upgradeButton.SetActive(false);
        }
        else
        {
            upgradeButton.SetActive(true);
        }
        changeTypeHUD.GetComponent<ChangeType>().openHUD = gameObject;
    }

    public void Unpick()
    {
        train = null;
    }

    //Add new wagon in HUD
    public void SetWagons(int wagonRef)
    {
        changeTypeHUD.GetComponent<ChangeType>().objectToChange = train.GetComponentInChildren<Locomotive>().wagons[wagonRef].gameObject;
        changeTypeHUD.SetActive(true);
        changeTypeHUD.GetComponent<ChangeType>().noTypeButton.SetActive(false);
        for (var i = 0; i != _pickedLocomotive.wagons.Count; i++)
        {
            if (_pickedLocomotive.wagons[i].gameObject.GetComponent<MeshRenderer>().enabled == true)
            {
                wagonsHUD.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                wagonsHUD.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        PickTrain(train);
    }

    //HUD button effect
    public void Upgrade()
    {
        //train.transform.GetChild(trainLevel.currentLevel).gameObject.GetComponent<BoxCollider>().enabled = true;
        train.transform.GetChild(_pickedLocomotive.CurrentSpeedLevel).gameObject.GetComponent<BoxCollider>().enabled = true;
        //if (!train.transform.GetChild(trainLevel.currentLevel).gameObject.GetComponent<Wagon>().hasTriggered)
        if (_pickedLocomotive.CurrentWagonCountLevel < _pickedLocomotive.TrainStats.MaxWagonCount)
        {
            //trainLevel.currentLevel++;
            _pickedLocomotive.UpgradeWagonCountLevel();
            text.SetText("Level " + _pickedLocomotive.CurrentWagonCountLevel);
            //if (trainLevel.currentLevel >= trainLevel.maxLevel)
            if (_pickedLocomotive.CurrentWagonCountLevel < _pickedLocomotive.TrainStats.MaxWagonCount)
            {
                upgradeButton.SetActive(false);
            }
            else
            {
                upgradeButton.SetActive(true);
            }
            //train.transform.GetChild(trainLevel.currentLevel).gameObject.GetComponent<MeshRenderer>().enabled = true;
            //train.transform.GetChild(trainLevel.currentLevel).gameObject.GetComponent<BoxCollider>().enabled = true;
            PickTrain(train);
            //LevelReferences.Instance.ScoreManager.AddScore(trainLevel.scoreToGiveOnUpgrade);
        }
        else
        {
            //train.transform.GetChild(trainLevel.currentLevel).gameObject.GetComponent<Wagon>().hasTriggered = false;
        }
    }

    public void DestroyTrain()
    {
        bool canDestroy = true;
        foreach (GameObject trainReferencedInStation in LevelReferences.Instance.Station.GetComponent<StationBehaviour>().trains)
        {
            if (trainReferencedInStation == train)
            {
                canDestroy = false;
            }
        }
        if(canDestroy)
        {
            GetComponentInParent<GoldManager>().CollectMoney(train.GetComponentInChildren<Locomotive>().Price / 2);
            gameObject.SetActive(false);
            changeTypeHUD.SetActive(false);
            Destroy(train);
        }
    }

    //Set wagons resources text & images
    private void Update()
    {
        for (var i = 0; i != _pickedLocomotive.wagons.Count; i++)
        {
            if (_pickedLocomotive.wagons[i].gameObject.GetComponent<MeshRenderer>().enabled == true)
            {
                wagonsHUD.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().SetText(train.GetComponentInChildren<Locomotive>().wagons[i].projectiles + "");
                wagonsHUD.transform.GetChild(i).GetChild(1).GetComponent<Image>().sprite = train.GetComponentInChildren<Locomotive>().wagons[i].type.icon;
            }
        }
    }
}
