using UnityEngine;

public class StationBehaviour : MonoBehaviour
{
    public int trainInStation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInChildren<Wagon>() == true)
        {
            trainInStation++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<Wagon>() == true)
        {
            trainInStation--;
        }
    }
}
