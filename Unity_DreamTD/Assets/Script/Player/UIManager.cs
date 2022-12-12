using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform _enemiesHealthBarContainer;

    [SerializeField]
    private Transform _mouseFollowerContainer;

    [SerializeField]
    private Transform _upgradePanelsContainer;

    [Header("Prefab")]
    [SerializeField]
    private GameObject _healthBarPrefab;

    [SerializeField]
    private UI_TowerPanelManager _towerUpgradePanel;

    [SerializeField]
    private UI_FactoryPanelManager _factoryUpgradePanel;

    [SerializeField]
    private TowerManagerPanel _towerManagerPanel;

    [SerializeField]
    private FactoryManagerPanel _factoryManagerPanel;
    

    //References
    private UI_TowerPanelManager upgradeTowerPanel = null;
    private UI_FactoryPanelManager upgradeFactoryPanel = null;

    private TowerManagerPanel towerManagerPanel;
    private FactoryManagerPanel factoryManagerPanel;

    public EnemiesHealthBar CreateEnemiesHealthBar(Transform target)
    {

        GameObject healthBar = Instantiate(_healthBarPrefab, _enemiesHealthBarContainer);

        healthBar.GetComponent<FollowOnScreen>().SetTarget(target);
        EnemiesHealthBar enemiesHealthBar = healthBar.GetComponent<EnemiesHealthBar>();
        return enemiesHealthBar;

    }

    public TowerManagerPanel CreateTowerPanel(TowerManager towerManager)
    {
        towerManagerPanel = Instantiate(_towerManagerPanel, _upgradePanelsContainer);

        towerManagerPanel.CreatePanel(towerManager);

        return (towerManagerPanel);
        
    }

    public FactoryManagerPanel CreateFactoryPanel(FactoryManager factoryManager)
    {
        factoryManagerPanel = Instantiate(_factoryManagerPanel, _upgradePanelsContainer);

        factoryManagerPanel.CreatePanel(factoryManager);

        return (factoryManagerPanel);
    }


    //Information
    public UI_TowerPanelManager CreateTowerUpgradeInformation(TowersDatas towerToUp)
    {
        if (upgradeTowerPanel == null)
        {
            upgradeTowerPanel = Instantiate(_towerUpgradePanel, _mouseFollowerContainer);

            upgradeTowerPanel.SetTowerData(towerToUp);
            
        }

        return (upgradeTowerPanel);
    }

    public UI_FactoryPanelManager CreateFactoryUpgradeInformation(FactoryDatas FactoryToUp)
    {
        if (upgradeFactoryPanel == null)
        {
            upgradeFactoryPanel = Instantiate(_factoryUpgradePanel, _mouseFollowerContainer);

            upgradeFactoryPanel.SetFactoryData(FactoryToUp);

        }

        return (upgradeFactoryPanel);
    }


    public void RemoveTowerUpgradeInformation()
    {
        upgradeTowerPanel.FadeOut();
    }


    public void DestroyAllUpgradeChildren()
    {
        for (int i = 0; i < _upgradePanelsContainer.transform.childCount; i++)
        {
            Transform child = _upgradePanelsContainer.transform.GetChild(i);

            if (child.GetComponent<TowerManagerPanel>() != null)
            {
                child.GetComponent<TowerManagerPanel>().ClosePanel();
                child.GetComponent<TowerManagerPanel>().TowerManager.gameObject.GetComponent<Tower>().RangeIndicator.EnableRangeIndicator(false);
            }
            else if (child.GetComponent<FactoryManagerPanel>() != null)
            {
                child.GetComponent<FactoryManagerPanel>().ClosePanel();
            }
        }
    }
}
