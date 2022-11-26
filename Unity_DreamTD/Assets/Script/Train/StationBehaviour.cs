
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBehaviour : MonoBehaviour
{
    [SerializeField] private Transform station;
    [SerializeField] private GameObject train;
    [SerializeField] private SplineDone spline;
    public List<Locomotive> locomotive;

    public void NewTrain()
    {
        var newTrain = Instantiate(train, station.position, Quaternion.identity);
        newTrain.transform.GetChild(0).GetComponent<SplineFollower>().spline = spline;
        newTrain.transform.GetChild(1).GetComponent<SplineFollower>().spline = spline;
        var locomotiveOfTrain = newTrain.GetComponentInChildren<Locomotive>();
        locomotive.Add(locomotiveOfTrain);
    }
}
