using System.Collections.Generic;
using UnityEngine;

//Made by Melinon Remy
public class StationBehaviour : MonoBehaviour
{
    [HideInInspector] public int trainInStation;
    [HideInInspector] public List<GameObject> trains;

    
    private Locomotive locomotive;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInChildren<Wagon>() == true)
        {
            trainInStation++;
            trains.Add(other.transform.parent.gameObject);

            bool IsRunning = LevelReferences.Instance.SpawnerManager.isWaveRunning;

            if (IsRunning == false)
            {
                locomotive = other.transform.root.GetChild(0).GetComponent<Locomotive>();
                locomotive.SetIsParked(true);
                locomotive.StopTrain(3);
            }
        }
    }

    public void RestartTrain()
    {
        if(locomotive != null)
        {
            locomotive.SetIsParked(false);
            locomotive.StartTrain();
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
