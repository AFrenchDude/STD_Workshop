using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class SkillTreeBehaviour : MonoBehaviour
{
    //Number of skill points
    [SerializeField] private TextMeshProUGUI text;
    //Ref to upgrader and player points
    [SerializeField] private ProjectileUpgradeData projectileUpgrade;
    [SerializeField] private PlayerScoreSave playerScore;
    //Dropdowns of selected upgrades
    [SerializeField] private TMP_Dropdown trapsDropdown;
    [SerializeField] private TMP_Dropdown energyDropdown;
    [SerializeField] private TMP_Dropdown pizzaDropdown;

    public void OnOpenSkillShop()
    {
        text.SetText("Skill points : " + playerScore.StarNumber);
        //Trap
        SetOptions(trapsDropdown, projectileUpgrade.unlockedTraps, projectileUpgrade.TrapUpgradeSelected + "");
        //Food
        SetOptions(pizzaDropdown, projectileUpgrade.unlockedFood, projectileUpgrade.FoodUpgradeSelected + "");
        //Energy
        SetOptions(energyDropdown, projectileUpgrade.unlockedEnergy, projectileUpgrade.EnergyUpgradeSelected + "");
    }

    //Set alredy unlocked option in HUD
    void SetOptions(TMP_Dropdown dropdown, List<string> unlockedList, string compareTo)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(unlockedList);
        for (int i = 0; i != unlockedList.Count; i++)
        {
            if (unlockedList[i] == compareTo)
            {
                dropdown.value = i;
            }
        }
    }

    //Unlocks
    public void UnlockTrapSkill(GameObject buttonObject)
    {
        if(playerScore.StarNumber >= buttonObject.GetComponent<ProjectileUpgradeSetter>().Price)
        {
            buttonObject.GetComponent<ProjectileUpgradeSetter>().SetTrapUpgrade();
            buttonObject.transform.GetChild(0).gameObject.SetActive(false);

            //SetTraps
            AddOption(projectileUpgrade.unlockedTraps, buttonObject.GetComponent<ProjectileUpgradeSetter>().trapUpgrade + "", trapsDropdown, buttonObject);
        }
    }
    public void UnlockFoodSkill(GameObject buttonObject)
    {
        if (playerScore.StarNumber >= buttonObject.GetComponent<ProjectileUpgradeSetter>().Price)
        {
            buttonObject.GetComponent<ProjectileUpgradeSetter>().SetFoodUpgrade();
            buttonObject.transform.GetChild(0).gameObject.SetActive(false);

            //SetFood
            AddOption(projectileUpgrade.unlockedFood, buttonObject.GetComponent<ProjectileUpgradeSetter>().foodUpgrade + "", pizzaDropdown, buttonObject);
        }
    }
    public void UnlockEnergySkill(GameObject buttonObject)
    {
        if (playerScore.StarNumber >= buttonObject.GetComponent<ProjectileUpgradeSetter>().Price)
        {
            buttonObject.GetComponent<ProjectileUpgradeSetter>().SetEnergyUpgrade();
            buttonObject.transform.GetChild(0).gameObject.SetActive(false);

            //SetEnergy
            AddOption(projectileUpgrade.unlockedEnergy, buttonObject.GetComponent<ProjectileUpgradeSetter>().energyUpgrade + "", energyDropdown, buttonObject);
        }
    }

    //Unlock new upgrade and update HUD
    void AddOption(List<string> unlockedList, string compareTo, TMP_Dropdown dropdown, GameObject button)
    {
        playerScore.RemoveStars(1);
        unlockedList.Add(compareTo);
        button.GetComponent<Button>().enabled = false;
        dropdown.ClearOptions();
        List<string> energyOptions = new List<string>();
        for (int i = 0; i != unlockedList.Count; i++)
        {
            energyOptions.Add(unlockedList[i] + "");
        }
        dropdown.AddOptions(energyOptions);
        for (int i = 0; i != unlockedList.Count; i++)
        {
            if (unlockedList[i] == compareTo)
            {
                dropdown.value = i;
            }
        }
        text.SetText("Skill points : " + playerScore.StarNumber);
    }

    //Set currently selected skill
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

    //Reset all upgrades and gives back skill points
    public void ResetSkillPoints()
    {
        //All skill point used minus basic upgrades
        int totalSkillPoints = projectileUpgrade.unlockedFood.Count + projectileUpgrade.unlockedEnergy.Count + projectileUpgrade.unlockedTraps.Count - 3;
        //If the player bought 1 or more upgrades
        if (totalSkillPoints > 0)
        {
            //Reset all unlocked upgrades list
            projectileUpgrade.unlockedFood.Clear();
            projectileUpgrade.unlockedFood.Add("None");
            projectileUpgrade.unlockedEnergy.Clear();
            projectileUpgrade.unlockedEnergy.Add("None");
            projectileUpgrade.unlockedTraps.Clear();
            projectileUpgrade.unlockedTraps.Add("None");

            //Set current upgrades to "basic"
            projectileUpgrade.SetEnergyUpgrade(ProjectileUpgradeData.EnergyUpgrades.Basic);
            projectileUpgrade.SetFoodUpgrade(ProjectileUpgradeData.FoodUpgrades.Basic);
            projectileUpgrade.SetTrapUpgrade(ProjectileUpgradeData.TrapUpgrades.Basic);

            //Enable buttons to rebuy upgrades
            foreach (Transform child in transform)
            {
                if (child.GetComponentInChildren<ProjectileUpgradeSetter>() != null)
                {
                    foreach (Transform child2 in child)
                    {
                        if (child2.childCount > 0)
                        {
                            child2.GetChild(0).gameObject.SetActive(true);
                        }
                        if (child2.GetComponent<Button>() != null)
                        {
                            child2.GetComponent<Button>().enabled = true;
                        }
                    }
                }
            }
            //Give back all skill points used
            playerScore.AddNewStars(totalSkillPoints);
            //Refresh HUD
            OnOpenSkillShop();
        }
    }
}
