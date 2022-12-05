//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileUpgradeManager : MonoBehaviour
{
    [SerializeField] private ProjectileType.projectileType _selectedProjectileType = ProjectileType.projectileType.Neutral;
    [SerializeField] private ProjectileUpgradeData _upgradeManager = null;

    private void Awake()
    {
        if (_upgradeManager != null)
        {
            EnableUpgrade();
        }
    }

    private void EnableUpgrade()
    {
        ADamagerEffect[] upgradeList = GetComponents<ADamagerEffect>();
        switch (_selectedProjectileType)
        {
            case ProjectileType.projectileType.Neutral:
                foreach (var upgrade in upgradeList)
                {
                    //If selected upgrade is basic enable nothing, otherwise every upgrade will trigger as upgrades of wrong type have enum value == basic
                    if (_upgradeManager.NeutralUpgradeSelected == ProjectileUpgradeData.NeutralUpgrades.Basic)
                    {
                        return;
                    }
                    if (_upgradeManager.NeutralUpgradeSelected == upgrade.GetNeutralUpgradeValue())
                    {
                        upgrade.enabled = true;
                    }
                }
                return;

            case ProjectileType.projectileType.Food:
                foreach (var upgrade in upgradeList)
                {
                    if (_upgradeManager.FoodUpgradeSelected == ProjectileUpgradeData.FoodUpgrades.Basic)
                    {
                        return;
                    }
                    if (_upgradeManager.FoodUpgradeSelected == upgrade.GetFoodUpgradeValue())
                    {
                        upgrade.enabled = true;
                    }
                }
                return;

            case ProjectileType.projectileType.Energy:
                foreach (var upgrade in upgradeList)
                {
                    if (_upgradeManager.EnergyUpgradeSelected == ProjectileUpgradeData.EnergyUpgrades.Basic)
                    {
                        return;
                    }
                    if (_upgradeManager.EnergyUpgradeSelected == upgrade.GetEnergyUpgradeValue())
                    {
                        upgrade.enabled = true;
                    }
                }
                return;

            case ProjectileType.projectileType.Trap:
                foreach (var upgrade in upgradeList)
                {
                    if (_upgradeManager.EnergyUpgradeSelected == ProjectileUpgradeData.EnergyUpgrades.Basic)
                    {
                        return;
                    }
                    if (_upgradeManager.TrapUpgradeSelected == upgrade.GetTrapUpgradeValue())
                    {
                        upgrade.enabled = true;
                    }
                }
                return;
        }
    }
}
