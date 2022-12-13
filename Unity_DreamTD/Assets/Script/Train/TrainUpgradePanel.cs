//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainUpgradePanel : MonoBehaviour
{
    [SerializeField] TrainLevelUpRequirements _trainUpgradeLevelUpRequirements = null;
    [SerializeField] Slider _trainEXPSlider = null;
    private List<WagonHUD> _wagonHUDList = new List<WagonHUD>();
    private int _indexLocomotiveDisplayed = 0;
	[SerializeField] private int _waveSurvivedCounter = 0;
    private bool _hasANextUpgrade = true;


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

    private void Awake()
    {
        UpdateWagonHUDList();
        _trainUpgradeLevelUpRequirements = Instantiate(_trainUpgradeLevelUpRequirements);
        UpdateCurrentWave();
    }

    private void Update() 
    {
        if (Locomotives.Count > 0)
        {
            UpdateHUDValues();//Ouch, added too late in dev to correctly link it to every event and skip the upgdate
        }
    }

    private void SetIndexLocomotiveDisplayed(int newIndex) //If we decide to include multiple train support
    {
        _indexLocomotiveDisplayed = newIndex;
        UpdateHUDValues();
    }

    [ContextMenu("UpdateHUDValues")]
    private void UpdateHUDValues()
    {
        for (int i = 0; i < _wagonHUDList.Count; i++)
        {
            bool shouldDisplayWagonHUD = Locomotives[_indexLocomotiveDisplayed].wagons[i].gameObject.activeSelf;
            _wagonHUDList[i].gameObject.SetActive(shouldDisplayWagonHUD);


            _wagonHUDList[i].Icon.sprite = Locomotives[_indexLocomotiveDisplayed].wagons[i].type.icon;
            _wagonHUDList[i].Background.sprite = Locomotives[_indexLocomotiveDisplayed].wagons[i].type.PrjectileBackgroundSprite;
            _wagonHUDList[i].CurrentStorageValueSlider.image.color = Locomotives[_indexLocomotiveDisplayed].wagons[i].type.ProjectileColor - new Color(.5f,.5f,.5f,0f);

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

    public void NewWavePassed()
    {
        if (_hasANextUpgrade == false)
        {
            return;
        }

        _waveSurvivedCounter++;
        if (_waveSurvivedCounter >= _trainUpgradeLevelUpRequirements.GetNextLevelUpRequirement())
        {

            ApplyUpgrades();

        }
        UpdateCurrentWave();
    }
    
    private void UpdateCurrentWave()
    {
        if (_hasANextUpgrade == false)
        {
            return;
        }

        _trainEXPSlider.value = _waveSurvivedCounter;
        _trainEXPSlider.maxValue = _trainUpgradeLevelUpRequirements.GetNextLevelUpRequirement();
    }

    private void ApplyUpgrades()
    {
        _waveSurvivedCounter = 0;
        TrainLevelUpRequirements.TrainUpgradeStructure levelUpData = _trainUpgradeLevelUpRequirements.GetLevelUpAction();
        if (levelUpData.upgradeLocoSpeed)
        {
            UpgradeEverySpeed();
        }
        if (levelUpData.upgradeWagonCount)
        {
            UpgradeWagonCount();
        }
        if (levelUpData.upgradeWagonStorage)
        {
            UpgradeMaxStorage();
        }
        if (levelUpData.upgradeLocoMesh)
        {
            UpgradeMesh();
        }
        _trainUpgradeLevelUpRequirements.LevelUp(out bool hasANextUpgrade);
        if (hasANextUpgrade == false)
        {
            _trainEXPSlider.gameObject.SetActive(hasANextUpgrade);
            _hasANextUpgrade = hasANextUpgrade; ;
        }
    }

    #region Upgrades
    [ContextMenu("UpgradeWagonCount")]
    public void UpgradeWagonCount()
    {
        Locomotives[_indexLocomotiveDisplayed].UpgradeWagonCountLevel();
        UpdateHUDValues();
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

    public void UpgradeMesh()
    {
        foreach (var locomotive in Locomotives)
        {
            locomotive.UpgradeMeshVisual();
        }
    }
    #endregion

    public void SetNewProjectileType(int wagonIndex, ProjectileType newType)
    {
        Locomotives[_indexLocomotiveDisplayed].wagons[wagonIndex].type = newType;
        Locomotives[_indexLocomotiveDisplayed].wagons[wagonIndex].SetWagonMesh();
        Locomotives[_indexLocomotiveDisplayed].wagons[wagonIndex].projectiles = 0;
    }

}
