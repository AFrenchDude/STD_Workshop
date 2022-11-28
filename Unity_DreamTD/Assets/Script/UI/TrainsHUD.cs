using TMPro;
using UnityEngine;

//Made by Melinon Remy
public class TrainsHUD : MonoBehaviour
{
    public GameObject train;
    public TextMeshProUGUI text;
    [SerializeField] private GameObject wagonsHUD;
    [SerializeField] private GameObject changeTypeHUD;
    [SerializeField] private GameObject upgradeButton;
    private TrainLevel trainLevel;

    //HUD info
    public void PickTrain()
    {
        trainLevel = train.transform.GetComponentInChildren<TrainLevel>();
        text.SetText("Level " + trainLevel.currentLevel);
        trainLevel = train.transform.GetComponentInChildren<TrainLevel>();
        for (var i = 0; i != train.GetComponentInChildren<Locomotive>().wagons.Count; i++)
        {
            if (train.GetComponentInChildren<Locomotive>().wagons[i].gameObject.GetComponent<MeshRenderer>().enabled == true)
            {
                wagonsHUD.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                wagonsHUD.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if (trainLevel.currentLevel >= trainLevel.maxLevel)
        {
            upgradeButton.SetActive(false);
        }
        else
        {
            upgradeButton.SetActive(true);
        }
        changeTypeHUD.GetComponent<ChangeType>().openHUD = gameObject;
    }

    //Add new wagon in HUD
    public void SetWagons(int wagonRef)
    {
        changeTypeHUD.GetComponent<ChangeType>().objectToChange = train.GetComponentInChildren<Locomotive>().wagons[wagonRef].gameObject;
        changeTypeHUD.SetActive(true);
        for (var i = 0; i != train.GetComponentInChildren<Locomotive>().wagons.Count; i++)
        {
            if (train.GetComponentInChildren<Locomotive>().wagons[i].gameObject.GetComponent<MeshRenderer>().enabled == true)
            {
                wagonsHUD.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                wagonsHUD.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        PickTrain();
    }

    //HUD button effect
    public void Upgrade()
    {
        trainLevel.currentLevel++;
        text.SetText("Level " + trainLevel.currentLevel);
        if (trainLevel.currentLevel >= trainLevel.maxLevel)
        {
            upgradeButton.SetActive(false);
        }
        else
        {
            upgradeButton.SetActive(true);
        }
        train.transform.GetChild(trainLevel.currentLevel).gameObject.GetComponent<MeshRenderer>().enabled = true;
        PickTrain();
    }

    public void DestroyTrain()
    {
        gameObject.SetActive(false);
        changeTypeHUD.SetActive(false);
        Destroy(train);
    }

    private void Update()
    {
        for (var i = 0; i != train.GetComponentInChildren<Locomotive>().wagons.Count; i++)
        {
            if (train.GetComponentInChildren<Locomotive>().wagons[i].gameObject.GetComponent<MeshRenderer>().enabled == true)
            {
                wagonsHUD.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().SetText(train.GetComponentInChildren<Locomotive>().wagons[i].projectiles + "");
            }
        }
    }
}
