//By ALBERT Esteban
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileUpgradeSetter : MonoBehaviour
{
    [SerializeField] ProjectileUpgradeData _projectileUpgradeData = null;
    [SerializeField] private ProjectileUpgradeData.NeutralUpgrades _neutralUpgradeSelect;
    [SerializeField] private ProjectileUpgradeData.EnergyUpgrades _energyUpgradeSelected;
    [SerializeField] private ProjectileUpgradeData.FoodUpgrades _foodUpgradeSelected;
    [SerializeField] private ProjectileUpgradeData.TrapUpgrades _trapUpgradeSelected;

    public ProjectileUpgradeData.TrapUpgrades trapUpgrade => _trapUpgradeSelected;
    public ProjectileUpgradeData.EnergyUpgrades energyUpgrade => _energyUpgradeSelected;
    public ProjectileUpgradeData.FoodUpgrades foodUpgrade => _foodUpgradeSelected;

    public void SetNeutralUpgrade()
    {
        _projectileUpgradeData.SetNeutralUpgrade(_neutralUpgradeSelect);
    }
    public void SetEnergyUpgrade()
    {
        _projectileUpgradeData.SetEnergyUpgrade(_energyUpgradeSelected);
    }
    public void SetFoodUpgrade()
    {
        _projectileUpgradeData.SetFoodUpgrade(_foodUpgradeSelected);
    }
    public void SetTrapUpgrade()
    {
        _projectileUpgradeData.SetTrapUpgrade(_trapUpgradeSelected);
    }

    //Check if upgrades ha been bought before to avoid buying twice
    private void Start()
    {
        CheckIfUnlocked(_projectileUpgradeData.unlockedTraps, _trapUpgradeSelected + "");
        CheckIfUnlocked(_projectileUpgradeData.unlockedFood, _foodUpgradeSelected + "");
        CheckIfUnlocked(_projectileUpgradeData.unlockedEnergy, _energyUpgradeSelected + "");
    }
    public void CheckIfUnlocked(List<string> unlockedList, string compareTo)
    {
        if(GetComponent<Button>() != null)
        {
            for (int i = 0; i != unlockedList.Count; i++)
            {
                if (unlockedList[i] == compareTo)
                {
                    Button skillButton = GetComponent<Button>();
                    skillButton.enabled = false;
                }
            }
        }
    }
}
