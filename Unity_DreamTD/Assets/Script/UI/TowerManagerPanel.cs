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

        UpdateTowerUpgrdePossibility();

    }

    public void UpdateTowerUpgrdePossibility()
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
            towerScriptRef.RangeIndicator.UpdateCircle();


            towerScriptRef.SetUpgradeMesh(_towerManager.TowersData.UpgradeDatas.UpgradePrefab);

            if (towerInformation != null)
            {
                towerInformation.SetTowerData(_towerManager.TowersData);
                towerInformation.CanUpgrade(canBuyTower);

            }
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


    //Projectiles
    [Header("Projectiles")]

    [SerializeField]
    private CurrentProjectileUI _projectilePrefab;

    [SerializeField]
    private List<Transform> _projectileContainers = new List<Transform>();

    public void CreateProjectiles()
    {
        int i = 0;
        foreach (Projectile projectile in _towerManager.TowersData.Projectiles)
        {
            CurrentProjectileUI newProjectile = Instantiate(_projectilePrefab, _projectileContainers[i].transform);

            newProjectile.SetUpProjectile(projectile);
            newProjectile.KeepReferences(_towerManager.TowersData, i);


            i++;
        }
    }

}
