using System.Collections.Generic;
using UnityEngine;

//Made by Melinon Remy
public class StationBehaviour : MonoBehaviour
{
    [SerializeField] private Transform station;
    [SerializeField] private GameObject train;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject trainHUD;
    [SerializeField] private GameObject createButton;
    public List<Locomotive> locomotive;
    [SerializeField] private int maxTrainsSpeed = 10;
    [SerializeField] private int currentTrainCreated;
    [SerializeField] private int maxTrainCreatable = 4;
    [SerializeField] private int trainPrice = 10;

    //Create train
    public void NewTrain()
    {
        if(currentTrainCreated < maxTrainCreatable && Base.Instance.Gold >= trainPrice)
        {
            Base.Instance.RemoveGold(trainPrice);
            trainPrice *= 2;
            currentTrainCreated++;
            //Create new train
            var newTrain = Instantiate(train, station.position, Quaternion.identity);
            newTrain.GetComponentInChildren<TrainLevel>().currentLevel = 1;
            newTrain.GetComponentInChildren<HUDwhenSelect>().hudRef = trainHUD;
            //Set path and speed
            foreach (Transform child in newTrain.transform)
            {
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
            newTrainInList.GetComponent<RectTransform>().localPosition = Vector3.zero;
            newTrainInList.GetComponent<RectTransform>().localScale = Vector3.one;
            if (currentTrainCreated >= maxTrainCreatable)
            {
                createButton.SetActive(false);
            }
        }
    }

    public void Upgrade()
    {

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
