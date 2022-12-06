using System.Collections.Generic;
using UnityEngine;

//Made by Melinon Remy
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
        if (currentTrainCreated < maxTrainCreatable && Base.Instance.Gold >= trainPrice && station.GetComponent<StationBehaviour>().trainInStation < 1)
        {
            Base.Instance.RemoveGold(trainPrice);
            currentTrainCreated++;
            //Create new train
            var newTrain = Instantiate(train, station.position, Quaternion.identity);
            newTrain.GetComponentInChildren<TrainLevel>().currentLevel = 1;
            //Set path and speed
            foreach (Transform child in newTrain.transform)
            {
                child.GetComponent<HUDwhenSelect>().hudRef = trainHUD;
                child.GetComponent<SplineFollower>().spline = LevelReferences.Instance.RailSpline;
                child.GetComponent<SplineFollower>().speed = maxTrainsSpeed;
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
            maxTrainsSpeed += 10;
            //Set speed
            foreach (Transform child in container.transform)
            {
                foreach (Transform child2 in child.GetComponent<TrainsHUD>().train.transform)
                {
                    if(child2.GetComponentInChildren<Locomotive>() != null)
                    {
                        child2.GetComponentInChildren<Locomotive>().maxSpeed = maxTrainsSpeed;
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
