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
    private List<TowerUpgradeData> _upgradeDatas;

    [SerializeField]
    private int _currentUpgradeIndex = 1;

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
    
    public List<TowerUpgradeData> UpgradeDatas => _upgradeDatas;
    public int CurrentUpgradeIndex => _currentUpgradeIndex;

   

    public List<Projectile> Projectiles => _projectileTypeList;

    public int MaxProjectilesAmmount => _maxProjectilesAmmount;
    
    public void Upgrade()
    {
        if(_currentUpgradeIndex < _upgradeDatas.Count - 1)
        {
            _currentUpgradeIndex++;
            ApplyUpgrade();
        }
        
    }

    public void ApplyUpgrade()
    {
        TowerUpgradeData upgrade = _upgradeDatas[_currentUpgradeIndex];

        _damage = upgrade.UpgradeDamage;
        _fireRate = upgrade.UpgradeFireRate;
        _range = upgrade.UpgradeRange;
        _maxProjectilesAmmount = upgrade.UpgradeMaxProjectiles;
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
    }

    //Test if a specific projectil has ammount
    public bool hasProjectiles(int index)
    {
        return _projectileTypeList[index].ProjectileAmmount > 0;
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
