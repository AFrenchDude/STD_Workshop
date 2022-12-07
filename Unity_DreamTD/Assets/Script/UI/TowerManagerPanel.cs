using System.Collections.Generic;
using UnityEngine;

public class TowerManagerPanel : MonoBehaviour
{
    [SerializeField]
    private TowerManager _towerManager;

    [Header("Information")]
    [SerializeField]
    private UI_TowerPanelManager _towerInfoPrefab;

    [SerializeField]
    private Transform _infoParent;


    private GoldManager goldManager;

    private void Awake()
    {
        goldManager = LevelReferences.Instance.Player.GetComponent<GoldManager>();
    }

    public void CreatePanel(TowerManager towerManager)
    {
        _towerManager = towerManager;
        transform.GetComponent<FollowOnScreen>().SetTarget(towerManager.CenterOfMass);

        CreateProjectiles();
    }

    public void DestroyPanel()
    {
        if (towerInformation != null)
        {
            towerInformation.transform.parent = transform.parent;
            towerInformation.FadeOut();
        }

        Destroy(gameObject);
    }

    public void SellTower()
    {

        goldManager.CollectMoney(_towerManager.TowersData.UpgradeDatas.UpgradePrice / 3);
        Destroy(_towerManager.gameObject);
        DestroyPanel();
    }

    public void Upgrade()
    {
        Tower towerScriptRef = _towerManager.transform.GetComponent<Tower>();
        if (goldManager.getFortune >= _towerManager.TowersData.UpgradeDatas.UpgradePrice & _towerManager.TowersData.UpgradeDatas.NextUpgrade != null)
        {
            string purchaseName = _towerManager.TowersData.name + "_Upgrade_" + _towerManager.TowersData.UpgradeDatas.name;
            goldManager.Buy(_towerManager.TowersData.UpgradeDatas.UpgradePrice, purchaseName);

            _towerManager.TowersData.Upgrade();
            _towerManager.ApplyStats(_towerManager.TowersData);
            towerScriptRef.RangeIndicator.UpdateCircle();


            towerScriptRef.SetUpgradeMesh(_towerManager.TowersData.UpgradeDatas.UpgradePrefab);

            if (towerInformation != null)
            {
                towerInformation.SetTowerData(_towerManager.TowersData);

            }
        }
    }

    //Informations

    private UI_TowerPanelManager towerInformation;

    public void CreateTowerUpgradeInformation()
    {
        if (towerInformation == null)
        {
            towerInformation = Instantiate(_towerInfoPrefab, _infoParent);

            towerInformation.SetTowerData(_towerManager.TowersData);

        }
    }

    public void RemoveTowerUpgradeInformation()
    {
        towerInformation.FadeOut();
    }


    //Projectiles
    [Header("Projectiles")]

    [SerializeField]
    private CurrentProjectileUI _projectilePrefab;

    [SerializeField]
    private List<Transform> _projectileContainers = new List<Transform>();

    public void CreateProjectiles()
    {
        int i = 0;
        foreach(Projectile projectile in _towerManager.TowersData.Projectiles)
        {
            CurrentProjectileUI newProjectile = Instantiate(_projectilePrefab, _projectileContainers[i].transform);

            newProjectile.SetUpProjectile(projectile);
            newProjectile.KeepReferences(_towerManager.TowersData, i);


            i++;
        }
    }

}
