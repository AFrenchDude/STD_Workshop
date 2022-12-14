using System.Collections.Generic;
using UnityEngine;

//Made by Dijoux Kevin
public class UpgradeType : MonoBehaviour
{
    private GameObject type_;

    [SerializeField]
    private List<GameObject> upgradeTypeHUD;

    public void SetType(GameObject type)
    {
        type_ = type.GetComponent<HUDwhenSelect>().hudRef;

        //If click on train
        if (type_.GetComponent<TrainsHUD>() != null)
        {
            upgradeTypeHUD[0].SetActive(true);
        }
        //If click on usine
        else if (type_.GetComponent<UsineHUD>() != null)
        {
            upgradeTypeHUD[1].SetActive(true);
        }
        //If click on tower
        else if (type_.GetComponent<TowerHUD>() != null)
        {
            //upgradeTypeHUD[2].SetActive(true);
        }
        //If click on station
        else if (type_.GetComponent<StationHUD>() == null)
        {
            upgradeTypeHUD[3].SetActive(true);
        }
    }

    public void ResetUpgrade()
    {
        type_ = null;
        upgradeTypeHUD[0].SetActive(false);
        upgradeTypeHUD[1].SetActive(false);
        upgradeTypeHUD[2].SetActive(false);
        upgradeTypeHUD[3].SetActive(false);
    }
}
