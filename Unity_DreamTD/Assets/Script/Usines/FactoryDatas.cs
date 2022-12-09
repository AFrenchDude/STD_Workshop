using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Factories/FactoryDatas", fileName = "FactoryDatas")]
public class FactoryDatas : ScriptableObject
{
    [SerializeField]
    private Sprite _sprite = null;

    [SerializeField]
    private string _name = null;

    [SerializeField]
    private string _description;

    [SerializeField]
    private string _type = null;

    [SerializeField]
    private ProjectileType _projectileType;

    [Header("Upgrades")]
    [SerializeField]
    private FactoryUpgradeData _currentUpgrade;

    [SerializeField]
    private int _ammount;

    [SerializeField]
    private int _maxAmmount;

    [SerializeField]
    private float _productionRate;

    [SerializeField]
    private bool _isProduction = false;

    [SerializeField]
    private int _sellPrice;

    [SerializeField] private int scoreToGiveOnUpgrade;

    [SerializeField] private bool _factoryDataUnlocked = false;


    //References
    public Sprite Icon => _sprite;
    public string Name => _name;
    public string UpgradeDescription => _description;
    public string Type => _type;
    
    public ProjectileType ProjectileType => _projectileType;
    public int Ammount => _ammount;
    public int MaxAmmount => _maxAmmount;
    public float ProductionRate => _productionRate;
    public bool IsProducing => _isProduction;
    public int SellPrice => _sellPrice;
    public FactoryUpgradeData CurrentUpgrade => _currentUpgrade;
    public bool IsUnlocked => _factoryDataUnlocked;

    public void UnlockFactoryrData()
    {
        _factoryDataUnlocked = true;
    }
    public void ResetStats()
    {
        _factoryDataUnlocked = false;
    }

    private void Awake()
    {
        _ammount = 0;
    }
    //Functions

    public void AddProjectile(int ammount)
    {
        _ammount += ammount;
    }

    public void RemoveProjectile(int ammount)
    {
        _ammount -= ammount;
    }

    public void SetProjectileAmmount(int ammount)
    {
        _ammount = ammount;
    }

    public void SetProductionEnable(bool productionEnable)
    {
        _isProduction = productionEnable;
    }

    public void Upgrade()
    {
        if(_currentUpgrade.NextUpgrade != null)
        {
            _currentUpgrade = _currentUpgrade.NextUpgrade;

            ApplyUpgrade();
        }
    }

    public void ManualUpgrade(FactoryUpgradeData upgradeToApply)
    {
        if (upgradeToApply != null)
        {
            _currentUpgrade = upgradeToApply;

            ApplyUpgrade();
        }
    }

    public void ApplyUpgrade()
    {
        LevelReferences.Instance.ScoreManager.AddScore(scoreToGiveOnUpgrade);
        _productionRate = _currentUpgrade.UpgradeCooldown;
        _maxAmmount = _currentUpgrade.UpgradeMaxResource;
        _sellPrice += _currentUpgrade.UpgradePrice;
    }
}