using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class MuseumButton : MonoBehaviour
{
    //Enemies
    [SerializeField] private NightmareBestiaryData nightmareBestiary;
    public NightmareBestiaryData NightmareBestiary => nightmareBestiary;

    //Usine
    [SerializeField] private FactoryDatas factoryData;
    public FactoryDatas FactoryData => factoryData;
    [SerializeField] private List<FactoryUpgradeData> factoryUpgradeData;
    public List<FactoryUpgradeData> FactoryUpgradeData => factoryUpgradeData;

    //Tower
    [SerializeField] private TowersDatas towersDatas;
    public TowersDatas TowersDatas => towersDatas;
    [SerializeField] private List<TowerUpgradeData> towersUpgradeData;
    public List<TowerUpgradeData> TowersUpgradeDatas => towersUpgradeData;

    //Set button icon
    private void Start()
    {
        if (nightmareBestiary != null)
        {
            if (nightmareBestiary.NightmareData.icon != null)
            {
                GetComponent<Image>().sprite = nightmareBestiary.NightmareData.icon;
            }
        }
        if (factoryData != null)
        {
            if (factoryData.Icon != null)
            {
                GetComponent<Image>().sprite = factoryData.Icon;
            }
        }
        if (towersDatas != null)
        {
            if (towersDatas.Icon != null)
            {
                GetComponent<Image>().sprite = towersDatas.Icon;
            }
        }
    }
}
