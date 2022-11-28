using UnityEngine;

//Made by Melinon Remy
public class HUDwhenSelect : MonoBehaviour
{
    public GameObject hudRef;
    
    //Display HUD
    public void OnClick(bool isActive)
    {
        //If click on train
        if (hudRef.GetComponent<TrainsHUD>() != null)
        {
            hudRef.SetActive(isActive);
            hudRef.GetComponent<TrainsHUD>().train = transform.parent.gameObject;
            hudRef.GetComponent<TrainsHUD>().PickTrain();
        }
        //If click on usine
        else if(hudRef.GetComponent<UsineHUD>() != null)
        {
            hudRef.SetActive(isActive);
            hudRef.GetComponent<UsineHUD>().usineBehaviour = GetComponent<UsineBehaviour>();
            hudRef.GetComponent<UsineHUD>().OnPick();
        }
        //If click on tower
        else if(hudRef.GetComponent<TowerHUD>() != null && gameObject.GetComponent<Tower>().enabled == true)
        {
            hudRef.SetActive(isActive);
            hudRef.GetComponent<TowerHUD>().tower = transform.gameObject;
            hudRef.GetComponent<TowerHUD>().OnPick();
        }
        //If click on station
        else if (hudRef.GetComponent<TowerHUD>() == null)
        {
            hudRef.SetActive(isActive);
        }
    }
}
