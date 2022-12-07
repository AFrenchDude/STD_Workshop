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
    private TowerManagerPanel _towerManagerPanel;
    

    //References
    private UI_TowerPanelManager upgradePanel = null;
    private TowerManagerPanel towerManagerPanel;

    public EnemiesHealthBar CreateEnemiesHealthBar(Transform target)
    {

        GameObject healthBar = Instantiate(_healthBarPrefab, _enemiesHealthBarContainer);

        healthBar.GetComponent<FollowOnScreen>().SetTarget(target);
        EnemiesHealthBar enemiesHealthBar = healthBar.GetComponent<EnemiesHealthBar>();
        return enemiesHealthBar;

    }

    public TowerManagerPanel CreateTowerPanel(TowerManager towerManager)
    {
        if(towerManagerPanel == null)
        {
            towerManagerPanel = Instantiate(_towerManagerPanel, _upgradePanelsContainer);

            towerManagerPanel.CreatePanel(towerManager);

            return (towerManagerPanel);
        }
        return null;
        
    }


    //Information
    public UI_TowerPanelManager CreateTowerUpgradeInformation(TowersDatas towerToUp)
    {
        if (upgradePanel == null)
        {
            upgradePanel = Instantiate(_towerUpgradePanel, _mouseFollowerContainer);

            upgradePanel.SetTowerData(towerToUp);
            
        }

        return (upgradePanel);
    }

    public void RemoveTowerUpgradeInformation()
    {
        upgradePanel.FadeOut();
    }
}
