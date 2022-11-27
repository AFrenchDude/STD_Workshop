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
            hudRef.GetComponentInChildren<LevelTrainText>().trainLevel = hudRef.GetComponent<TrainsHUD>().train.GetComponentInChildren<TrainLevel>();
            hudRef.GetComponent<TrainsHUD>().PickTrain();
        }
        else if(hudRef.GetComponent<UsineHUD>() != null)
        {
            hudRef.GetComponent<UsineHUD>().usineBehaviour = GetComponent<UsineBehaviour>();
            hudRef.GetComponent<UsineHUD>().OnPick();
        }
    }
}
