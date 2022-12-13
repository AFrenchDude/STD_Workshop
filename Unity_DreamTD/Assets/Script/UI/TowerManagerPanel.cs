using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Animator _animator;

    [Header("UI Economy")]
    [SerializeField]
    private Image _upgradeImage;

    [Space(10)]

    [SerializeField]
    private Sprite _upgradeSprite;
    [SerializeField]
    private Sprite _lockedSprite;

    [Space(10)]

    [SerializeField]
    private Color _canBuyColor;
    [SerializeField]
    private Color _cantBuyColor;

    public TowerManager TowerManager => _towerManager;

    private void Awake()
    {
        goldManager = LevelReferences.Instance.Player.GetComponent<GoldManager>();
        _animator = GetComponent<Animator>();
    }

    public void CreatePanel(TowerManager towerManager)
    {
        _towerManager = towerManager;
        transform.GetComponent<FollowOnScreen>().SetTarget(towerManager.CenterOfMass);

        CreateProjectiles();

        int i = 0;
        foreach (Projectile projectile in _towerManager.TowersData.ProjectilesList)
        {
            _projectilePrefab.SetUpProjectile(_towerManager.TowersData.ProjectilesList[i]);
            i++;
        }
        UpdateTowerUpgradePossibility();
    }

    public void UpdateTowerUpgradePossibility()
    {
        if (towerHasUpgrade)
        {
            _upgradeImage.sprite = _upgradeSprite;

            if (canBuyTower)
            {
                _upgradeImage.color = _canBuyColor;
            }
            else
            {
                _upgradeImage.color = _cantBuyColor;
            }
        }
        else
        {
            _upgradeImage.color = Color.white;
            _upgradeImage.sprite = _lockedSprite;
        }
    }


    public bool canBuyTower
    {
        get { return _towerManager.TowersData.UpgradeDatas.UpgradePrice <= goldManager.getFortune; }
    }

    public bool towerHasUpgrade
    {
        get { return _towerManager.TowersData.canUpgrade; }
    }

    public void ClosePanel()
    {
        _animator.SetBool("Close", true);
    }

    public void DestroyPanel()
    {
        if (_animator.GetBool("Close"))
        {
            if (towerInformation != null)
            {
                towerInformation.transform.parent = transform.parent;
                towerInformation.FadeOut();
            }

            Destroy(gameObject);
        }
    }

    public void SellTower()
    {
        goldManager.CollectMoney(_towerManager.TowersData.UpgradeDatas.UpgradePrice / 3);
        Destroy(_towerManager.gameObject);
        ClosePanel();
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

            towerScriptRef.SetUpgradeMesh(_towerManager.TowersData.UpgradeDatas.UpgradePrefab);

            if (towerInformation != null & towerHasUpgrade)
            {
                towerInformation.SetTowerData(_towerManager.TowersData);
                towerInformation.CanUpgrade(canBuyTower);
                CreateProjectiles();
            }
            else
            {
                UpdateTowerUpgradePossibility();
            }

            towerScriptRef.RangeIndicator.UpdateCircle();
        }
    }

    //Informations
    private UI_TowerPanelManager towerInformation;

    public void CreateTowerUpgradeInformation()
    {
        if (towerHasUpgrade)
        {
            if (towerInformation == null)
            {
                towerInformation = Instantiate(_towerInfoPrefab, _infoParent);

                towerInformation.SetTowerData(_towerManager.TowersData);
                towerInformation.CanUpgrade(canBuyTower);
                UpdateTowerUpgradePossibility();
            }
        }
    }

    public void RemoveTowerUpgradeInformation()
    {
        if (towerInformation != null)
        {
            towerInformation.FadeOut();
        }
    }


    //ProjectilesList
    [Header("ProjectilesList")]

    [SerializeField]
    private CurrentProjectileUI _projectilePrefab;

    [SerializeField]
    private List<Transform> _projectileContainers = new List<Transform>();

    public void CreateProjectiles()
    {
        List<CurrentProjectileUI> createdUi = new List<CurrentProjectileUI>();
        int i = 0;
        foreach (Projectile projectile in _towerManager.TowersData.ProjectilesList)
        {
            CurrentProjectileUI newProjectile = Instantiate(_projectilePrefab, _projectileContainers[i].transform);

            newProjectile.SetUpProjectile(projectile);
            newProjectile.KeepReferences(_towerManager.TowersData, i);

            createdUi.Add(newProjectile);

            i++;
        }

        foreach (CurrentProjectileUI projectile in createdUi)
        {
            {
                projectile.SetOtherProjectilesPreview(createdUi);
            }
        }

    }

}
