using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Factories/FactoryDatas", fileName = "FactoryDatas")]
public class FactoryDatas : ScriptableObject
{

    [SerializeField]
    private ProjectileType _projectileType;

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

    [SerializeField]
    private FactoryUpgradeData _currentUpgrade;


    //References
    public ProjectileType ProjectileType => _projectileType;
    public int Ammount => _ammount;
    public int MaxAmmount => _maxAmmount;
    public float ProductionRate => _productionRate;
    public bool IsProducing => _isProduction;
    public int SellPrice => _sellPrice;
    public FactoryUpgradeData CurrentUpgrade => _currentUpgrade;


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

    public void ApplyUpgrade()
    {
        _productionRate = _currentUpgrade.UpgradeCooldown;
        _maxAmmount = _currentUpgrade.UpgradeMaxResource;
        _sellPrice += _currentUpgrade.UpgradePrice;
    }
}