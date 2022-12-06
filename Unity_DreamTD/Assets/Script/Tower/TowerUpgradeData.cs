//By ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DreamTD/TowerUpgrade", fileName = "TowerUpgrade")]
public class TowerUpgradeData : ScriptableObject
{
    [Header("Personnalize")]
    [SerializeField]
    private string _upgradeName;

    [Header("Stats")]
    [SerializeField]
    private float _upgradeDamage;

    [SerializeField]
    private float _upgradeFireRate;

    [SerializeField]
    private float _upgradeRange;

    [SerializeField]
    private int _upgradeMaxProjectiles;

    [Header("Mortar")]
    [SerializeField]
    private float _upgradeAOERadius;

    [Space(20)]
    [Header("Economy")]

    [SerializeField]
    private int _upgradePrice;

    [SerializeField]
    private GameObject _upgradePrefab;

    [SerializeField]
    private TowerUpgradeData _nextUpgrade;

    //Public references

    public string UpgradeName => _upgradeName;

    public float UpgradeDamage => _upgradeDamage;
    public float UpgradeFireRate => _upgradeFireRate;
    public float UpgradeRange => _upgradeRange;
    public int UpgradeMaxProjectiles => _upgradeMaxProjectiles;
    public float UpgradeAOERadius => _upgradeAOERadius;
    public int UpgradePrice => _upgradePrice;
    public GameObject UpgradePrefab => _upgradePrefab;
    public TowerUpgradeData NextUpgrade => _nextUpgrade;

}
