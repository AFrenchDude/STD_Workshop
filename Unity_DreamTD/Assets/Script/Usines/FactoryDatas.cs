using System.Collections;
using System.Collections.Generic;
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
    private bool _isProduction;

    [SerializeField]
    private int _sellPrice;

    [SerializeField]
    private List<FactoryUpgradeData> _upgradeData = new List<FactoryUpgradeData>();


    //References
    public ProjectileType ProjectileType => _projectileType;
    public int Ammount => _ammount;
    public int MaxAmmount => _maxAmmount;
    public float ProductionRate => _productionRate;
    public bool IsProducing => _isProduction;
    public int SellPrice => _sellPrice;
    public List<FactoryUpgradeData> UpgradeData => _upgradeData;


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

}