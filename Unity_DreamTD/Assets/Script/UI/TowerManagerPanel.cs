using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Made by Alexandre Dorian
public class TowerManagerPanel : MonoBehaviour
{
    [SerializeField]
    private TowerManager _towerManager;

    [Header("Information")]
    [SerializeField]
    private UI_TowerPanelManager _towerInfoPrefab;

    [SerializeField]
    private Transform _infoParent;

    [SerializeField]
    private GameObject _infoCurrentTowerPrefab = null;

    private Transform _infoFactoryAnchor;
    private float _infoFactorySetupTime = 0.0f;
    private bool _isWaitingForInfoFactory = false;

    private GoldManager goldManager;
    private Animator _animator;
    private GameObject currentPanel = null;

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

    private void OnEnable()
    {
        goldManager.FortuneChanged.RemoveListener(UpdateTowerUpgradePossibility);
        goldManager.FortuneChanged.AddListener(UpdateTowerUpgradePossibility);
    }
    private void OnDisable()
    {
        goldManager.FortuneChanged.RemoveListener(UpdateTowerUpgradePossibility);
    }

    public void CreatePanel(TowerManager towerManager, Transform infoTowerAnchor)
    {
        _towerManager = towerManager;
        _infoFactoryAnchor = infoTowerAnchor;

        currentPanel = Instantiate(_infoCurrentTowerPrefab, _infoFactoryAnchor);
        SetInfoTower();

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
        towerInformation?.CanUpgrade(canBuyTower);
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
        Destroy(currentPanel.gameObject);

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

        SetInfoTower();
    }

    public void SetInfoFactoryAnchor(Transform infoFactoryAnchor) // See HUDWhen Select calls
    {
        _infoFactoryAnchor = infoFactoryAnchor;
        ClearOtherInfoCurrentFactory();
        _infoFactorySetupTime = Time.time;
        _isWaitingForInfoFactory = true;
    }

    private void ClearOtherInfoCurrentFactory()
    {
        //Debug.Log("Anchor has: " + _infoFactoryAnchor.childCount + "children");
        for (int i = 0; i < _infoFactoryAnchor.childCount; i++)
        {
            Debug.Log("Try destroy child: " + (_infoFactoryAnchor.GetChild(i).gameObject));
            if (_infoFactoryAnchor.GetChild(i).GetComponent<InfoCurrentFactory>() != null)
            {
                Destroy(_infoFactoryAnchor.GetChild(i).gameObject);
            }
        }
    }

    public void SetInfoTower()
    {
        //Debug.Log("Set info capable? " + (currentPanel.name));
        InfoCurrentTower infoCurrentTower = currentPanel.GetComponent<InfoCurrentTower>();

        infoCurrentTower.Name.text = _towerManager.TowersData.UpgradeDatas.UpgradeName;
        infoCurrentTower.Damage.text = _towerManager.TowersData.Damage.ToString();
        infoCurrentTower.Firerate.text = _towerManager.TowersData.FireRate.ToString();
        infoCurrentTower.Range.text = _towerManager.TowersData.Range.ToString();
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
