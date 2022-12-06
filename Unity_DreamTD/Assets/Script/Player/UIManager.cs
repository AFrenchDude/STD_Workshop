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

    [Header("Prefab")]
    [SerializeField]
    private GameObject _healthBarPrefab;

    [SerializeField]
    private UI_TowerPanelManager _towerUpgradePanel;


    public EnemiesHealthBar CreateEnemiesHealthBar(Transform target)
    {

        GameObject healthBar = Instantiate(_healthBarPrefab, _enemiesHealthBarContainer);

        healthBar.GetComponent<FollowOnScreen>().SetTarget(target);
        EnemiesHealthBar enemiesHealthBar = healthBar.GetComponent<EnemiesHealthBar>();
        return enemiesHealthBar;

    }

    public UI_TowerPanelManager CreateTowerUpgradePanel(TowersDatas towerToUp)
    {
        UI_TowerPanelManager upgradePanel = Instantiate(_towerUpgradePanel, _mouseFollowerContainer);

        upgradePanel.SetTowerData(towerToUp);

        return(upgradePanel);
    }
}
