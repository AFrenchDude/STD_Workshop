using System.Collections.Generic;
using UnityEngine;

//Made by Melinon Remy
public class StationBehaviour : MonoBehaviour
{
    [HideInInspector] public int trainInStation;
    [HideInInspector] public List<GameObject> trains;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInChildren<Wagon>() == true)
        {
            trainInStation++;
            trains.Add(other.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<Wagon>() == true)
        {
            trainInStation--;
            trains.Remove(other.transform.parent.gameObject);
        }
    }
}
