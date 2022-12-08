using System.Collections.Generic;
using UnityEngine;

//Made by Melinon Remy, modified by ALBERT Esteban to update stats via S.O
public class StationHUD : MonoBehaviour
{
    private Transform station;
    [HideInInspector] public List<Locomotive> locomotive;
    [SerializeField] private GameObject train;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject trainHUD;
    [SerializeField] private GameObject createButton;
    [SerializeField] private GameObject upgradeButton;
    private int currentTrainCreated;
    private int stationLevel = 1;
    [SerializeField] private int maxStationLevel = 3;
    [SerializeField] private int maxTrainCreatable = 2;
    [SerializeField] private int maxTrainsSpeed = 10;
    [SerializeField] private int trainPrice = 10;

    private void Start()
    {
        station = LevelReferences.Instance.Station.transform;
        
    }

    //Create train
    public void NewTrain()
    {
        if (currentTrainCreated < maxTrainCreatable && LevelReferences.Instance.Player.GetComponent<GoldManager>()._currentFortune >= trainPrice && station.GetComponent<StationBehaviour>().trainInStation < 1)
        {
            LevelReferences.Instance.Player.GetComponent<GoldManager>().Buy(trainPrice, "train");
            currentTrainCreated++;
            //Create new train
            var newTrain = Instantiate(train, station.position, Quaternion.identity);
            Locomotive newLocomotive = newTrain.GetComponentInChildren<Locomotive>();
            newLocomotive.SetSpeedLevel(stationLevel);
            newLocomotive.SetStorageLevel(stationLevel);

            //Set path and speed
            newTrain.transform.GetComponentInChildren<SplineFollower>().spline = LevelReferences.Instance.RailSpline;
            foreach (Transform child in newTrain.transform)
            {
                child.GetComponent<HUDwhenSelect>().hudRef = trainHUD;
            }

            var locomotiveOfTrain = newTrain.GetComponentInChildren<Locomotive>();
            locomotive.Add(locomotiveOfTrain);
            //Add new train to list
            var newTrainInList = Instantiate(trainHUD);
            newTrainInList.SetActive(true);
            newTrainInList.GetComponent<TrainsHUD>().train = newTrain;
            newTrainInList.GetComponent<RectTransform>().SetParent(container.transform);
            newTrainInList.GetComponent<RectTransform>().localScale = Vector3.one;
            newTrainInList.GetComponent<RectTransform>().localPosition = Vector3.zero;
            newTrainInList.GetComponent<RectTransform>().localRotation = Quaternion.identity;

            if (currentTrainCreated >= maxTrainCreatable)
            {
                createButton.SetActive(false);
            }
        }
    }

    public void Upgrade()
    {
        if(stationLevel < maxStationLevel)
        {
            stationLevel++;
            maxTrainCreatable++;
            //Set speed
            foreach (Transform child in container.transform)
            {
                foreach (Transform child2 in child.GetComponent<TrainsHUD>().train.transform)
                {
                    if(child2.GetComponentInChildren<Locomotive>() != null)
                    {
                        child2.GetComponentInChildren<Locomotive>().UpgradeSpeedLevel();
                        child2.GetComponentInChildren<Locomotive>().UpgradeStorageLevel();
                    }
                }
            }
        }
        if (currentTrainCreated < maxTrainCreatable)
        {
            createButton.SetActive(true);
        }
        if (stationLevel >= maxStationLevel)
        {
            upgradeButton.SetActive(false);
        }
    }

    //Set trains list
    private void Update()
    {
        for (var i = 0; i != container.transform.childCount; i++)
        {
            if (container.transform.GetChild(i).GetComponent<TrainsHUD>().train != null)
            {
                container.transform.GetChild(i).GetComponent<TrainsHUD>().PickTrain(container.transform.GetChild(i).GetComponent<TrainsHUD>().train);
            }
            else
            {
                currentTrainCreated--;
                Destroy(container.transform.GetChild(i).gameObject);
                createButton.SetActive(true);
            }
        }
    }
}
