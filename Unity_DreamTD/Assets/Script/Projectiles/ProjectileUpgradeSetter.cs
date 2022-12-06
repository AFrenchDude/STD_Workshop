//By ALBERT Esteban
using UnityEngine;

public class ProjectileUpgradeSetter : MonoBehaviour
{
    [SerializeField] ProjectileUpgradeData _projectileUpgradeData = null;
    [SerializeField] private ProjectileUpgradeData.NeutralUpgrades _neutralUpgradeSelect;
    [SerializeField] private ProjectileUpgradeData.EnergyUpgrades _energyUpgradeSelecte;
    [SerializeField] private ProjectileUpgradeData.FoodUpgrades _foodUpgradeSelected;
    [SerializeField] private ProjectileUpgradeData.TrapUpgrades _trapUpgradeSelected;

    public ProjectileUpgradeData.TrapUpgrades trapUpgrade => _trapUpgradeSelected;
    public ProjectileUpgradeData.EnergyUpgrades energyUpgrade => _energyUpgradeSelecte;
    public ProjectileUpgradeData.FoodUpgrades foodUpgrade => _foodUpgradeSelected;

    public void SetNeutralUpgrade()
    {
        _projectileUpgradeData.SetNeutralUpgrade(_neutralUpgradeSelect);
    }
    public void SetEnergyUpgrade()
    {
        _projectileUpgradeData.SetEnergyUpgrade(_energyUpgradeSelecte);
    }
    public void SetFoodUpgrade()
    {
        _projectileUpgradeData.SetFoodUpgrade(_foodUpgradeSelected);
    }
    public void SetTrapUpgrade()
    {
        _projectileUpgradeData.SetTrapUpgrade(_trapUpgradeSelected);
    }
}
