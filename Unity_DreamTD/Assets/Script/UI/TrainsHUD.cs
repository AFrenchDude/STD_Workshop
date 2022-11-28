using TMPro;
using UnityEngine;

public class TrainsHUD : MonoBehaviour
{
    public GameObject train;
    public TextMeshProUGUI text;
    [SerializeField] private GameObject wagonsHUD;
    [SerializeField] private GameObject changeType;
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
        changeType.GetComponent<ChangeType>().openHUD = gameObject;
    }

    public void SetWagons(int wagonRef)
    {
        changeType.GetComponent<ChangeType>().objectToChange = train.GetComponentInChildren<Locomotive>().wagons[wagonRef].gameObject;
        changeType.SetActive(true);
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
        changeType.SetActive(false);
        Destroy(train);
    }
}
