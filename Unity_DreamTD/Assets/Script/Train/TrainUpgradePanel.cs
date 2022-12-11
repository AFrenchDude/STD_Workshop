//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainUpgradePanel : MonoBehaviour
{
    private List<WagonHUD> _wagonHUDList = new List<WagonHUD>();
    private int _indexLocomotiveDisplayed = 0;


    private List<Locomotive> Locomotives
    {
        get
        {
            return LevelReferences.Instance.LocomotiveManager.GetLocomotives;
        }
    }

    private void UpdateWagonHUDList()
    {
        _wagonHUDList.Clear();
        foreach (var wagon in GetComponentsInChildren<WagonHUD>())
        {
            _wagonHUDList.Add(wagon);
        }
    }
    private void SetIndexLocomotiveDisplayed(int newIndex) //If we decide to include multiple train support
    {
        _indexLocomotiveDisplayed = newIndex;
        UpdateHUDValues();
    }

    private void Awake()
    {
        UpdateWagonHUDList();
    }

    private void Update()
    {
        if (Locomotives.Count > 0)
        {
            UpdateHUDValues();
        }
    }

    [ContextMenu("UpdateHUDValues")]
    private void UpdateHUDValues()
    {
        for (int i = 0; i < _wagonHUDList.Count; i++)
        {
            bool shouldDisplayWagonHUD = Locomotives[_indexLocomotiveDisplayed].wagons[i].gameObject.activeSelf;
            _wagonHUDList[i].gameObject.SetActive(shouldDisplayWagonHUD);


            _wagonHUDList[i].Icon.sprite = Locomotives[_indexLocomotiveDisplayed].wagons[i].type.icon;
            _wagonHUDList[i].CurrentStorageText.SetText("" + Locomotives[_indexLocomotiveDisplayed].wagons[i].projectiles);
            _wagonHUDList[i].MaxStorageText.SetText("" + Locomotives[_indexLocomotiveDisplayed].wagons[i].MaxWagonStorage);
            _wagonHUDList[i].SetSliderValue(Locomotives[_indexLocomotiveDisplayed].wagons[i].projectiles, Locomotives[_indexLocomotiveDisplayed].wagons[i].MaxWagonStorage);


            _wagonHUDList[i].CurrentProjectileUI.SetUpProjectile(
                Locomotives[_indexLocomotiveDisplayed].wagons[i].type,
                Locomotives[_indexLocomotiveDisplayed].wagons[i].projectiles,
                Locomotives[_indexLocomotiveDisplayed].wagons[i].MaxWagonStorage); //What does it do?

            _wagonHUDList[i].CurrentProjectileUI.SetRefToTrainPanel(this, i); //Why here in update?
        }
    }


    [ContextMenu("UpgradeWagonCount")]
    public void UpgradeWagonCount()
    {
        Locomotives[_indexLocomotiveDisplayed].UpgradeWagonCountLevel();
    }

    [ContextMenu("UpgradeMaxStorage")]
    public void UpgradeMaxStorage()
    {
        Locomotives[_indexLocomotiveDisplayed].UpgradeStorageLevel();
    }

    [ContextMenu("UpgradeEverySpeed")]
    public void UpgradeEverySpeed()
    {
        UpgradeSpeed(true);
    }

    [ContextMenu("UpgradeCurrentLocomotiveSpeed")]
    public void UpgradeCurrentLocomotiveSpeed()
    {
        UpgradeSpeed(false);
    }

    public void UpgradeSpeed(bool upgradeAllLocomotives)
    {
        if (upgradeAllLocomotives)
        {
            foreach (var locomotive in Locomotives)
            {
                locomotive.UpgradeSpeedLevel();
            }
        }
        else
        {
            Locomotives[_indexLocomotiveDisplayed].UpgradeSpeedLevel();
        }
    }

    public void SetNewProjectileType(int wagonIndex, ProjectileType newType)
    {
        Locomotives[_indexLocomotiveDisplayed].wagons[wagonIndex].type = newType;
        Locomotives[_indexLocomotiveDisplayed].wagons[wagonIndex].SetWagonMesh();
        Locomotives[_indexLocomotiveDisplayed].wagons[wagonIndex].projectiles = 0;
    }


}
