using UnityEngine;

//Made by Melinon Remy, modified by ALBERT Esteban
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
        if (LevelReferences.Instance.PlayerDrag.IsDragging == false)
        {
            OpenSelectedHUD();
        }
    }

    private void OpenSelectedHUD()
    {
        //If click on usine
        if (gameObject.transform.root.GetComponent<FactoryManager>() != null)
        {
            _currentFactoryManagerPanel = _uIManager.CreateFactoryPanel(GetComponent<FactoryManager>());
        }
        //If click on tower
        else if (gameObject.transform.root.GetComponent<Tower>() != null)
        {
            _currentTowerManagerPanel = _uIManager.CreateTowerPanel(GetComponent<TowerManager>());
            gameObject.GetComponent<Tower>().RangeIndicator.EnableRangeIndicator(true);
        }
    }

    public void OnDeselect()
    {
        //If usine
        if (gameObject.GetComponent<Factory>() != null)
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
            if (_currentTowerManagerPanel != null)
            {
                _currentTowerManagerPanel.ClosePanel();
            }
            if (gameObject.GetComponent<Tower>().enabled) //enabled == false => isDragging => prevent disabling RangeIndicator
            {
                gameObject.GetComponent<Tower>().RangeIndicator.EnableRangeIndicator(false);
            }
        }
    }
}
