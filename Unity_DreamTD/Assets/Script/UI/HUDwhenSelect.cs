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
    FactoryManagerPanel _currentFactoryManagerPanel = null;
    TowerManagerPanel _currentTowerManagerPanel = null;
    //FactoryManager

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
        else if(gameObject.GetComponent<FactoryManager>() != null)
        {         
            _currentFactoryManagerPanel = _uIManager.CreateFactoryPanel(GetComponent<FactoryManager>());
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
        else if (gameObject.GetComponent<Factory>() != null)
        {
            //hudRef.GetComponent<UsineHUD>().OnUnpick();
            //hudRef.SetActive(false);

            if (_currentFactoryManagerPanel != null)
            {
                _currentFactoryManagerPanel.ClosePanel();
            }
        }
        //If tower
        else if (gameObject.GetComponent<Tower>() != null)
        {
            if(_currentTowerManagerPanel != null)
            {
                _currentTowerManagerPanel.ClosePanel();
            }
        }
        //If station
        else if (hudRef.GetComponent<TowerHUD>() == null)
        {
            hudRef.SetActive(false);
        }
    }
}
