using UnityEngine;

//Made by Melinon Remy
public class ChangeType : MonoBehaviour
{
    public GameObject noTypeButton;
    public GameObject objectToChange;
    public GameObject openHUD;
    [SerializeField] private GameObject stationHUD;

    //Set new type
    public void ChangingType(ProjectileType type)
    {
        //For wagon
        if(objectToChange.GetComponent<Wagon>() != null)
        {
            objectToChange.GetComponent<Wagon>().type = type;
            objectToChange.GetComponent<Wagon>().projectiles = 0;
        }
        //For tower
        else if(objectToChange.GetComponentInChildren<TowerGetProjectile>() != null)
        {
            objectToChange.GetComponentInChildren<TowerGetProjectile>().type = type;
            objectToChange.GetComponentInChildren<TowerGetProjectile>().projectiles = 0;
            openHUD.GetComponent<TowerHUD>().OnPick(objectToChange);
        }
    }

    //Check if parent HUD is open, else close this one
    private void Update()
    {
        if(!openHUD.activeSelf || !stationHUD.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
