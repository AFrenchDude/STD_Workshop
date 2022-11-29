using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Factories/FactoryUpgrade", fileName = "FactoryUpgrade")]
public class FactoryUpgradeData : ScriptableObject
{
    [Header("Stats")]
    [SerializeField]
    private float _upgradeCooldown;

    [SerializeField]
    private int _upgradeMaxResource;

    [SerializeField]
    private int _upgradePrice;

    //References

    public float UpgradeCooldown => _upgradeCooldown;
    public int UpgradeMaxResource => _upgradeMaxResource;
    public int UpgradePrice => _upgradePrice;
}
