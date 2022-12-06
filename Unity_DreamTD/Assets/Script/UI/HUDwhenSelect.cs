using UnityEngine;

//Made by Melinon Remy
public class HUDwhenSelect : MonoBehaviour
{
    public GameObject hudRef;
    

    //Display HUD
    public void OnSelect()
    {
        //If click on train
        if (hudRef.GetComponent<TrainsHUD>() != null)
        {
            hudRef.SetActive(true);
            hudRef.GetComponent<TrainsHUD>().PickTrain(transform.parent.gameObject);
        }
        //If click on usine
        else if(hudRef.GetComponent<UsineHUD>() != null)
        {
            hudRef.SetActive(true);
            hudRef.GetComponent<UsineHUD>().OnPick(GetComponent<UsineBehaviour>());
        }
        //If click on tower
        else if(hudRef.GetComponent<TowerHUD>() != null && gameObject.GetComponent<Tower>().enabled == true)
        {
            hudRef.GetComponent<TowerHUD>().OnPick(transform.gameObject);
            hudRef.SetActive(true);
        }
        //If click on station
        else if (hudRef.GetComponent<TowerHUD>() == null)
        {
            hudRef.SetActive(true);
        }
    }

    public void OnDeselect()
    {
        //If train
        if (hudRef.GetComponent<TrainsHUD>() != null)
        {
            hudRef.GetComponent<TrainsHUD>().Unpick();
            hudRef.SetActive(false);
        }
        //If usine
        else if (hudRef.GetComponent<UsineHUD>() != null)
        {
            hudRef.GetComponent<UsineHUD>().OnUnpick();
            hudRef.SetActive(false);
        }
        //If tower
        else if (hudRef.GetComponent<TowerHUD>() != null && gameObject.GetComponent<Tower>().enabled == true)
        {
            hudRef.GetComponent<TowerHUD>().OnUnpick();
            hudRef.SetActive(false);
        }
        //If station
        else if (hudRef.GetComponent<TowerHUD>() == null)
        {
            hudRef.SetActive(false);
        }
    }
}
