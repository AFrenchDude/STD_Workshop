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
    private void SetIndexLocomotiveDisplayed(int newIndex)
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
        Debug.Log("Locomotives: " + Locomotives.Count);
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
            _wagonHUDList[i].CurrentStorageText.SetText("" + Locomotives[_indexLocomotiveDisplayed].wagons[i].projectiles);
            _wagonHUDList[i].MaxStorageText.SetText("" + Locomotives[_indexLocomotiveDisplayed].wagons[i].MaxWagonStorage);
        }
    }

    [ContextMenu("UpgradeMaxStorage")]
    private void UpgradeMaxStorage()
    {
        Locomotives[_indexLocomotiveDisplayed].UpgradeStorageLevel();
    }

    [ContextMenu("UpgradeEverySpeed")]
    private void UpgradeEverySpeed()
    {
        UpgradeSpeed(true);
    }

    [ContextMenu("UpgradeCurrentLocomotiveSpeed")]
    private void UpgradeCurrentLocomotiveSpeed()
    {
        UpgradeSpeed(false);
    }

    private void UpgradeSpeed(bool upgradeAllLocomotives)
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
    }
}
