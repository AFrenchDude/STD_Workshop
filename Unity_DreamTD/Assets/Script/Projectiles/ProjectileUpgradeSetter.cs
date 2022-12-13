//By ALBERT Esteban
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileUpgradeSetter : MonoBehaviour
{
    [SerializeField] ProjectileUpgradeData _projectileUpgradeData = null;
    [SerializeField] private ProjectileUpgradeData.NeutralUpgrades _neutralUpgradeSelect;
    [SerializeField] private ProjectileUpgradeData.EnergyUpgrades _energyUpgradeSelected;
    [SerializeField] private ProjectileUpgradeData.FoodUpgrades _foodUpgradeSelected;
    [SerializeField] private ProjectileUpgradeData.TrapUpgrades _trapUpgradeSelected;
    [SerializeField] private int _price = 1;
    [SerializeField] private TextMeshProUGUI textPrice;

    public ProjectileUpgradeData.TrapUpgrades trapUpgrade => _trapUpgradeSelected;
    public ProjectileUpgradeData.EnergyUpgrades energyUpgrade => _energyUpgradeSelected;
    public ProjectileUpgradeData.FoodUpgrades foodUpgrade => _foodUpgradeSelected;
    public int Price => _price;

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
        foreach (string unlockedTraps in _projectileUpgradeData.unlockedTraps)
        {
            if(unlockedTraps == _trapUpgradeSelected + "")
            {
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            }
        }
        foreach (string unlockedPizza in _projectileUpgradeData.unlockedFood)
        {
            if (unlockedPizza == _foodUpgradeSelected + "")
            {
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            }
        }
        foreach (string unlockedEnergy in _projectileUpgradeData.unlockedEnergy)
        {
            if (unlockedEnergy == _energyUpgradeSelected + "")
            {
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            }
        }
        if (textPrice != null)
        {
            textPrice.SetText("X" + _price);
        }
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
