//By ALBERT Esteban, modified by MELINON Remy
using System.Collections.Generic;
using TMPro;
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

    //Already unlocked upgrades
    public List<string> unlockedFood;
    public List<string> unlockedEnergy;
    public List<string> unlockedTraps;

    //Currently used upgrades
    [SerializeField] private NeutralUpgrades _neutralUpgradeSelected;
    [SerializeField] private EnergyUpgrades _energyUpgradeSelected;
    [SerializeField] private FoodUpgrades _foodUpgradeSelected;
    [SerializeField] private TrapUpgrades _trapUpgradeSelected;
    public NeutralUpgrades NeutralUpgradeSelected => _neutralUpgradeSelected;
    public EnergyUpgrades EnergyUpgradeSelected => _energyUpgradeSelected;
    public FoodUpgrades FoodUpgradeSelected => _foodUpgradeSelected;
    public TrapUpgrades TrapUpgradeSelected => _trapUpgradeSelected;

    //Set current upgrades by buying it
    public void SetNeutralUpgrade(NeutralUpgrades newNeutralUpgrade)
    {
        _neutralUpgradeSelected = newNeutralUpgrade;
    }
    public void SetEnergyUpgrade(EnergyUpgrades newEnergyUpgrade)
    {
        _energyUpgradeSelected = newEnergyUpgrade;
    }
    public void SetFoodUpgrade(FoodUpgrades newFoodUpgrade)
    {
        _foodUpgradeSelected = newFoodUpgrade;
    }
    public void SetTrapUpgrade(TrapUpgrades newTrapUpgrade)
    {
        _trapUpgradeSelected = newTrapUpgrade;
    }

    //Set current upgrades by dropdowns
    public void TrapDropdownValueChanged(TMP_Dropdown change)
    {
        _trapUpgradeSelected = (TrapUpgrades)change.value;
    }
    public void EnergyDropdownValueChanged(TMP_Dropdown change)
    {
        _energyUpgradeSelected = (EnergyUpgrades)change.value;
    }
    public void PizzaDropdownValueChanged(TMP_Dropdown change)
    {
        _foodUpgradeSelected = (FoodUpgrades)change.value;
    }
}
