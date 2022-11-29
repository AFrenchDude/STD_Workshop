//By ALEXANDRE Dorian
using System.Collections;
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

    [Header("Projectiles")]
    [SerializeField]
    private List<Projectile> _projectileTypeList = new List<Projectile>();

    [SerializeField]
    private int _maxProjectilesAmmount;


    public float Damage => _damage;
    public float FireRate => _fireRate;
    public float Range => _range;
    public float ProjectileSpeed => _projectileSpeed;
    public fireType FireType => _fireType;
    
    public TowerUpgradeData UpgradeDatas => _currentUpgrade;


   

    public List<Projectile> Projectiles => _projectileTypeList;

    public int MaxProjectilesAmmount => _maxProjectilesAmmount;
    
    public void Upgrade()
    {
        if(_currentUpgrade.NextUpgrade != null)
        {
            _currentUpgrade = _currentUpgrade.NextUpgrade;

            ApplyUpgrade();

        }

    }

    public void ApplyUpgrade()
    {
        _damage = _currentUpgrade.UpgradeDamage;
        _fireRate = _currentUpgrade.UpgradeFireRate;
        _range = _currentUpgrade.UpgradeRange;
        _maxProjectilesAmmount = _currentUpgrade.UpgradeMaxProjectiles;
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
}
