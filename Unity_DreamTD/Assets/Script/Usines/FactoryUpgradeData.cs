using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Factories/FactoryUpgrade", fileName = "FactoryUpgrade")]
public class FactoryUpgradeData : ScriptableObject
{
    [Header("Stats")]
    [SerializeField]
    private string _upgradeName;

    [SerializeField]
    private float _upgradeCooldown;

    [SerializeField]
    private int _upgradeMaxResource;

    [SerializeField]
    private int _upgradePrice;

    [SerializeField]
    private GameObject _upgradePrefab;

    [SerializeField]
    private FactoryUpgradeData _nextUpgrade;



    //References
    public string UpgradeName => _upgradeName;

    public float UpgradeCooldown => _upgradeCooldown;
    public int UpgradeMaxResource => _upgradeMaxResource;
    public int UpgradePrice => _upgradePrice;
    public GameObject UpgradePrefab => _upgradePrefab;
    public FactoryUpgradeData NextUpgrade => _nextUpgrade;
}
