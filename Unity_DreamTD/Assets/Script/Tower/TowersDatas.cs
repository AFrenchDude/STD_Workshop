//By ALEXANDRE Dorian
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DreamTD/TowersData", fileName = "TowersData")]
public class TowersDatas : ScriptableObject
{
    public enum fireType
    {
        Direct,
        DoubleCanon,
        Mortar
    }

    [Header("Stats")]

    [SerializeField]
    private Sprite _sprite = null;

    [SerializeField]
    private string _name = null;

    [SerializeField]
    private string _description = null;

    [SerializeField]
    private string _type = null;

    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _fireRate;

    [SerializeField]
    private float _range;

    [SerializeField]
    private float _projectileSpeed;

    [SerializeField]
    private fireType _fireType;

    [Header("Upgrades")]
    [SerializeField]
    private TowerUpgradeData _currentUpgrade;

    [Header("ProjectilesList")]
    [SerializeField]
    private List<Projectile> _projectileTypeList = new List<Projectile>();

    [SerializeField]
    private int _maxProjectilesAmmount;

    [Header("Mortar")]
    [SerializeField]
    private float _aoeRadius;

    [SerializeField] private int scoreToGiveOnUpgrade;

    [SerializeField] private bool _towerDataUnlocked = false;

    public Sprite Icon => _sprite;
    public string Name => _name;
    public string Description => _description;
    public string Type => _type;

    public float Damage => _damage;
    public float FireRate => _fireRate;
    public float Range => _range;
    public float ProjectileSpeed => _projectileSpeed;
    public fireType FireType => _fireType;

    public TowerUpgradeData UpgradeDatas => _currentUpgrade;

    public List<Projectile> ProjectilesList => _projectileTypeList;

    public int MaxProjectilesAmmount => _maxProjectilesAmmount;

    public float AOERadius => _aoeRadius;
    public bool IsUnlocked => _towerDataUnlocked;

    public void UnlockTowerData()
    {
        _towerDataUnlocked = true;
    }

    public void Upgrade()
    {
        if(_currentUpgrade.NextUpgrade != null)
        {
            _currentUpgrade = _currentUpgrade.NextUpgrade;
            ApplyUpgrade();
        }
    }
    public void ManualUpgrade(TowerUpgradeData upgradeToApply)
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
        _damage = _currentUpgrade.UpgradeDamage;
        _fireRate = _currentUpgrade.UpgradeFireRate;
        _range = _currentUpgrade.UpgradeRange;
        _maxProjectilesAmmount = Mathf.CeilToInt((float)_currentUpgrade.UpgradeMaxProjectiles / (float)_projectileTypeList.Count);
        _aoeRadius = _currentUpgrade.UpgradeAOERadius;

        foreach(Projectile projectile in _projectileTypeList)
        {
            projectile.MaxProjectilesAmmount = _maxProjectilesAmmount;
        }
    }

    public void SetProjAmmount(int index, int ammount)
    {
        _projectileTypeList[index].ProjectileAmmount = ammount;
    }
    public void AddProjAmmount(int index, int ammount)
    {
        _projectileTypeList[index].ProjectileAmmount += ammount;
    }
    public void ReduceProjAmmount(int index, int ammount)
    {
        _projectileTypeList[index].ProjectileAmmount -= ammount;
    }

    //Set Projectlile Type
    public void SetProjectileType(int index, ProjectileType projectileType)
    {
        _projectileTypeList[index].ProjectileType = projectileType;
        _projectileTypeList[index].ProjectileAmmount = 0;
    }

    //Test if a specific projectil has ammount
    public bool hasProjectiles(int index)
    {
        return _projectileTypeList[index].ProjectileAmmount > 0;
    }

    public bool canUpgrade
    {
        get 
        {
            return _currentUpgrade.NextUpgrade != null;
        }
    }
}

[System.Serializable]
public class Projectile
{
    [SerializeField]
    public ProjectileType ProjectileType;

    [SerializeField]
    public int ProjectileAmmount;

    [SerializeField]
    public int MaxProjectilesAmmount;

    public void SetupProjectile(ProjectileType type, int number, int maxAmmount)
    {
        ProjectileType = type;
        ProjectileAmmount = number;
        MaxProjectilesAmmount = maxAmmount;
    }
}
