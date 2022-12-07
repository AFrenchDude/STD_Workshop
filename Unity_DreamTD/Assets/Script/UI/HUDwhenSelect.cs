using UnityEngine;

//Made by Melinon Remy
public class HUDwhenSelect : MonoBehaviour
{
    public GameObject hudRef;

    private UIManager _uIManager;
    private void Awake()
    {
        _uIManager = LevelReferences.Instance.Player.GetComponent<UIManager>();
    }

    //References
    TowerManagerPanel _currentTowerManagerPanel = null;

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
        else if(gameObject.GetComponent<Tower>()!= null)
        {
            _currentTowerManagerPanel = _uIManager.CreateTowerPanel(GetComponent<TowerManager>());
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
        else if (gameObject.GetComponent<Tower>() != null)
        {
            if(_currentTowerManagerPanel != null)
            {
                _currentTowerManagerPanel.DestroyPanel();
            }
        }
        //If station
        else if (hudRef.GetComponent<TowerHUD>() == null)
        {
            hudRef.SetActive(false);
        }
    }
}
