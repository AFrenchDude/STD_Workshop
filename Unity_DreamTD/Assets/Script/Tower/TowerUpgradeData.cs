//By ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DreamTD/TowerUpgrade", fileName = "TowerUpgrade")]
public class TowerUpgradeData : ScriptableObject
{
    [Header("References")]
    //Serialized references
    [SerializeField]
    private int _currentUpgradeIndex;

    [SerializeField]
    private int _nextUpgradeIndex;


    [Header("Stats")]
    [SerializeField]
    private float _upgradeDamage;

    [SerializeField]
    private float _upgradeFireRate;

    [SerializeField]
    private float _upgradeRange;

    [SerializeField]
    private int _upgradeMaxProjectiles;

    //Public references
    public int CurrentUpgradeIndex => _currentUpgradeIndex;
    public int NextUpgradeIndex => _nextUpgradeIndex;
    public float UpgradeDamage => _upgradeDamage;
    public float UpgradeFireRate => _upgradeFireRate;
    public float UpgradeRange => _upgradeRange;
    public int UpgradeMaxProjectiles => _upgradeMaxProjectiles;

}
