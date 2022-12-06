//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/ProjectileUpgradeData", fileName = "ProjectileUpgradeData")]
public class ProjectileUpgradeData : ScriptableObject
{
    #region enums
    public enum NeutralUpgrades
    {
        Basic
    }

    public enum EnergyUpgrades
    {
        Basic,
        Bouncing
    }

    public enum FoodUpgrades
    {
        Basic,
        ExplosivePepperoni,
        SpicyHotPizza
    }

    public enum TrapUpgrades
    {
        Basic,
        GumArea
    }
    #endregion


    [SerializeField] private NeutralUpgrades _neutralUpgradeSelected;
    [SerializeField] private EnergyUpgrades _energyUpgradeSelected;
    [SerializeField] private FoodUpgrades _foodUpgradeSelected;
    [SerializeField] private TrapUpgrades _trapUpgradeSelected;

    public NeutralUpgrades NeutralUpgradeSelected => _neutralUpgradeSelected;
    public EnergyUpgrades EnergyUpgradeSelected => _energyUpgradeSelected;
    public FoodUpgrades FoodUpgradeSelected => _foodUpgradeSelected;
    public TrapUpgrades TrapUpgradeSelected => _trapUpgradeSelected;
}
