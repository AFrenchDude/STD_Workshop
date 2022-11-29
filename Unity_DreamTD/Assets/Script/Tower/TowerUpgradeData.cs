//By ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DreamTD/TowerUpgrade", fileName = "TowerUpgrade")]
public class TowerUpgradeData : ScriptableObject
{
    [Header("Stats")]
    [SerializeField]
    private float _upgradeDamage;

    [SerializeField]
    private float _upgradeFireRate;

    [SerializeField]
    private float _upgradeRange;

    [SerializeField]
    private int _upgradeMaxProjectiles;

    [SerializeField]
    private int _upgradePrice;

    //Public references
    public float UpgradeDamage => _upgradeDamage;
    public float UpgradeFireRate => _upgradeFireRate;
    public float UpgradeRange => _upgradeRange;
    public int UpgradeMaxProjectiles => _upgradeMaxProjectiles;
    public int UpgradePrice => _upgradePrice;

}
