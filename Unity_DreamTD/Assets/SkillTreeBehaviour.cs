using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class SkillTreeBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private ProjectileUpgradeData projectileUpgrade;
    [SerializeField] private TMP_Dropdown trapsDropdown;
    [SerializeField] private TMP_Dropdown energyDropdown;
    [SerializeField] private TMP_Dropdown pizzaDropdown;

    private void Start()
    {
        RefreshStats();
    }

    public void RefreshStats()
    {
        trapsDropdown.ClearOptions();
        trapsDropdown.AddOptions(projectileUpgrade.unlockedTraps);

        energyDropdown.ClearOptions();
        energyDropdown.AddOptions(projectileUpgrade.unlockedEnergy);

        pizzaDropdown.ClearOptions();
        pizzaDropdown.AddOptions(projectileUpgrade.unlockedFood);

        text.SetText("Skill points : " + projectileUpgrade.skillPoint);
    }

    public void UnlockTrapSkill(GameObject buttonObject)
    {
        if(projectileUpgrade.skillPoint >= 1)
        {
            projectileUpgrade.skillPoint--;
            buttonObject.GetComponent<ProjectileUpgradeSetter>().SetTrapUpgrade();
            projectileUpgrade.unlockedTraps.Add(buttonObject.GetComponent<ProjectileUpgradeSetter>().trapUpgrade + "");
            buttonObject.GetComponent<Button>().enabled = false;

            //SetTraps
            trapsDropdown.ClearOptions();
            List<string> trapsOptions = new List<string>();
            for (int i = 0; i != projectileUpgrade.unlockedTraps.Count; i++)
            {
                trapsOptions.Add(projectileUpgrade.unlockedTraps[i] + "");
            }
            trapsDropdown.AddOptions(trapsOptions);
            for (int i = 0; i != projectileUpgrade.unlockedTraps.Count; i++)
            {
                if (projectileUpgrade.unlockedTraps[i] == buttonObject.GetComponent<ProjectileUpgradeSetter>().trapUpgrade + "")
                {
                    trapsDropdown.value = i;
                }
            }
            RefreshStats();
        }
    }
    public void UnlockFoodSkill(GameObject buttonObject)
    {
        if (projectileUpgrade.skillPoint >= 1)
        {
            projectileUpgrade.skillPoint--;
            buttonObject.GetComponent<ProjectileUpgradeSetter>().SetFoodUpgrade();
            projectileUpgrade.unlockedFood.Add(buttonObject.GetComponent<ProjectileUpgradeSetter>().foodUpgrade + "");
            buttonObject.GetComponent<Button>().enabled = false;

            //SetFood
            pizzaDropdown.ClearOptions();
            List<string> pizzaOptions = new List<string>();
            for (int i = 0; i != projectileUpgrade.unlockedFood.Count; i++)
            {
                pizzaOptions.Add(projectileUpgrade.unlockedFood[i] + "");
            }
            pizzaDropdown.AddOptions(pizzaOptions);
            for (int i = 0; i != projectileUpgrade.unlockedFood.Count; i++)
            {
                if (projectileUpgrade.unlockedFood[i] == buttonObject.GetComponent<ProjectileUpgradeSetter>().foodUpgrade + "")
                {
                    pizzaDropdown.value = i;
                }
            }
            RefreshStats();
        }
    }
    public void UnlockEnergySkill(GameObject buttonObject)
    {
        if (projectileUpgrade.skillPoint >= 1)
        {
            projectileUpgrade.skillPoint--;
            buttonObject.GetComponent<ProjectileUpgradeSetter>().SetEnergyUpgrade();
            projectileUpgrade.unlockedEnergy.Add(buttonObject.GetComponent<ProjectileUpgradeSetter>().energyUpgrade + "");
            buttonObject.GetComponent<Button>().enabled = false;

            //SetEnergy
            energyDropdown.ClearOptions();
            List<string> energyOptions = new List<string>();
            for (int i = 0; i != projectileUpgrade.unlockedEnergy.Count; i++)
            {
                energyOptions.Add(projectileUpgrade.unlockedEnergy[i] + "");
            }
            energyDropdown.AddOptions(energyOptions);
            for (int i = 0; i != projectileUpgrade.unlockedEnergy.Count; i++)
            {
                if (projectileUpgrade.unlockedEnergy[i] == buttonObject.GetComponent<ProjectileUpgradeSetter>().energyUpgrade + "")
                {
                    energyDropdown.value = i;
                }
            }
            RefreshStats();
        }
    }

    public void PickTrapSkill(Int32 optionSelected)
    {
        projectileUpgrade.TrapDropdownValueChanged(trapsDropdown);
    }
    public void PickFoodSkill(Int32 optionSelected)
    {
        projectileUpgrade.PizzaDropdownValueChanged(pizzaDropdown);
    }
    public void PickEnergySkill(Int32 optionSelected)
    {
        projectileUpgrade.EnergyDropdownValueChanged(energyDropdown);
    }
}
