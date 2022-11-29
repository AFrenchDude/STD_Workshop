using UnityEngine;

//Made by Melinon Remy
public class ChangeType : MonoBehaviour
{
    public GameObject noTypeButton;
    public GameObject objectToChange;
    public GameObject openHUD;

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
            objectToChange.GetComponent<TowerManager>().TowersData.SetProjectileType(0, type);
            objectToChange.GetComponentInChildren<TowerGetProjectile>().projectiles = 0;
            openHUD.GetComponent<TowerHUD>().OnPick(objectToChange);
        }
    }

    //Check if parent HUD is open, else close this one
    private void Update()
    {
        //If from station
        if (openHUD.GetComponentInParent<StationHUD>() != null)
        {
            openHUD = openHUD.GetComponentInParent<StationHUD>().transform.gameObject;
        }
        if (!openHUD.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
