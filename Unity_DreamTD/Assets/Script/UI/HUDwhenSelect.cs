using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Made by Melinon Remy, modified by ALBERT Esteban
public class HUDwhenSelect : MonoBehaviour
{
    public GameObject hudRef;
    private UIManager _uIManager;

    [SerializeField]
    private GameObject infoCurrentFactory;

    private GameObject currentPanel;

    private Transform _anchor;

    public FactoryManagerPanel FactoryManagerPanel
    {
        get
        {
            return LevelReferences.Instance.Player.GetComponentInChildren<FactoryManagerPanel>();
        }
    }

    private void Awake()
    {
        _uIManager = LevelReferences.Instance.Player.GetComponent<UIManager>();
    }

    //References
    FactoryManagerPanel _currentFactoryManagerPanel = null;
    TowerManagerPanel _currentTowerManagerPanel = null;

    //Event Connexions
    //public void ConnectToEvent()
    //{
    //    FactoryManagerPanel?.updateUpgradefactory.RemoveListener(OnUpdateUpgradeFactory);
    //    FactoryManagerPanel?.updateUpgradefactory.AddListener(OnUpdateUpgradeFactory);
    //}
    //public void DisconnectToEvents() //Selector removes ref too quickly, need to forcefully call it before ref clear
    //{
    //    FactoryManagerPanel?.updateUpgradefactory.RemoveListener(OnUpdateUpgradeFactory);
    //}

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

            LevelReferences.Instance.Player.GetComponentInChildren<FactoryManagerPanel>().SetInfoFactoryAnchor(_anchor);
            //currentPanel = Instantiate(infoCurrentFactory, _anchor);
            //SetInfoFactory();
            //ConnectToEvent();
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
                Debug.Log("Deselecting");
                _currentFactoryManagerPanel.ClosePanel();
                //Destroy(currentPanel.gameObject);
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

    public void SetAnchor(Transform anchor)
    {
        _anchor = anchor;
    }

    //private void OnUpdateUpgradeFactory(bool isUpdate)
    //{
    //    if (isUpdate)
    //    {
    //        SetInfoFactory();
    //        return;
    //    }
    //    OnDeselect();
    //}

    //public void SetInfoFactory()
    //{
    //    currentPanel.GetComponent<InfoCurrentFactory>().Name.text =
    //        gameObject.transform.root.GetComponent<FactoryManager>().FactoryData.Name;
    //    currentPanel.GetComponent<InfoCurrentFactory>().Production.text =
    //        gameObject.transform.root.GetComponent<FactoryManager>().FactoryData.ProductionRate.ToString();
    //    currentPanel.GetComponent<InfoCurrentFactory>().MaxStorage.text =
    //        gameObject.transform.root.GetComponent<FactoryManager>().FactoryData.MaxAmmount.ToString();
    //}
}
