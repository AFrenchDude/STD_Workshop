using UnityEngine;

public class ChangeType : MonoBehaviour
{
    public GameObject objectToChange;
    public GameObject openHUD;

    //Set new type
    public void ChangingType(ProjectileType type)
    {
        if(objectToChange.GetComponent<Wagon>() != null)
        {
            objectToChange.GetComponent<Wagon>().type = type;
            objectToChange.GetComponent<Wagon>().projectiles.Clear();
        }
        else if(objectToChange.GetComponent<Tower>() != null)
        {
            objectToChange.GetComponent<TowerGetProjectile>().type = type;
        }
    }

    private void Update()
    {
        if(!openHUD.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
