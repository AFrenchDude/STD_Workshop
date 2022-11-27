
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBehaviour : MonoBehaviour
{
    [SerializeField] private Transform station;
    [SerializeField] private GameObject train;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject trainHUD;
    [SerializeField] private GameObject createButton;
    [SerializeField] private SplineDone spline;
    public List<Locomotive> locomotive;
    [SerializeField] private int maxTrainsSpeed = 10;
    [SerializeField] private int currentTrainCreated;
    [SerializeField] private int maxTrainCreatable = 4;

    public void NewTrain()
    {
        if(currentTrainCreated < maxTrainCreatable)
        {
            currentTrainCreated++;
            //Create new train
            var newTrain = Instantiate(train, station.position, Quaternion.identity);
            newTrain.GetComponentInChildren<TrainLevel>().currentLevel = 1;
            newTrain.GetComponentInChildren<HUDwhenSelect>().hudRef = trainHUD;
            //Set path and speed
            foreach (Transform child in newTrain.transform)
            {
                child.GetComponent<SplineFollower>().spline = spline;
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

    private void Update()
    {
        for (var i = 0; i != container.transform.childCount; i++)
        {
            if (container.transform.GetChild(i).GetComponent<TrainsHUD>().train != null)
            {
                container.transform.GetChild(i).GetComponent<TrainsHUD>().PickTrain();
            }
        }
    }
}
