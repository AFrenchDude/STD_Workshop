using UnityEngine;

//Made by Alexandre Dorian
public class MainMenu_RailsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _railPrefab;

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<MainMenu_RailsAnimation>() != null)
        {
            GameObject newRail = Instantiate(_railPrefab, transform.parent);
            newRail.transform.localPosition = transform.localPosition;
        }
    }
}
