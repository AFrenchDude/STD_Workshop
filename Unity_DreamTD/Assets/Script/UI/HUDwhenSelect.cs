using UnityEngine;

public class HUDwhenSelect : MonoBehaviour
{
    public GameObject hudRef;
    
    public void OnClick(bool isActive)
    {
        hudRef.SetActive(isActive);
        if (hudRef.GetComponent<TrainsHUD>() != null)
        {
            hudRef.GetComponent<TrainsHUD>().train = transform.parent.gameObject;
            hudRef.GetComponent<TrainsHUD>().PickTrain();
        }
        else if(hudRef.GetComponent<UsineHUD>() != null)
        {
            hudRef.GetComponent<UsineHUD>().usineBehaviour = GetComponent<UsineBehaviour>();
            hudRef.GetComponent<UsineHUD>().OnPick();
        }
    }
}
