using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class UsineHUD : MonoBehaviour
{
    public GameObject factory;

    [SerializeField] private FactoryDatas _factoryData;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image productionIcon;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider slider;
    [HideInInspector] public float productionValue;

    private Transform _factoryTransform;
    private float currentResources;
    private float maxResources;

    //Set resources text and slider
    private void Update()
    {
        text.SetText("Production: " + productionValue);
        if (_factoryData != null)
        {
            currentResources = _factoryData.Ammount;
            maxResources = _factoryData.MaxAmmount;
            slider.value = currentResources / maxResources;
        }
    }

    public void OnProductionValueChange(Single newValue)
    {
        productionValue = newValue * _factoryData.MaxAmmount;
    }

    public void OnPick(UsineBehaviour pickedUsine)
    {
        _factoryData = pickedUsine.getFactoryData;
        _factoryTransform = pickedUsine.transform;
        productionIcon.sprite = _factoryData.ProjectileType.icon;
        toggle.isOn = _factoryData.IsProducing;
        if (_factoryData.CurrentUpgrade == null || _factoryData.CurrentUpgrade.NextUpgrade == null)
        {
            upgradeButton.SetActive(false);
        }
        else
        {
            upgradeButton.SetActive(true);
        }
    }

    public void OnUnpick()
    {
        _factoryData = null;
    }

    public void SetOnOff(bool isOn)
    {
        _factoryData.SetProductionEnable(isOn);
    }

    public void Upgrade()
    {
        FactoryManager managedFactory = factory.GetComponent<FactoryManager>();
        GoldManager goldManager = LevelReferences.Instance.Player.GetComponent<GoldManager>();
        if (goldManager.getFortune >= _factoryData.CurrentUpgrade.UpgradePrice)
        {
            string purchaseName = _factoryData.name + "_Upgrade_" + _factoryData.CurrentUpgrade.name;
            goldManager.Buy(_factoryData.CurrentUpgrade.UpgradePrice, purchaseName);
            _factoryData.Upgrade();

            managedFactory.FactoryData.Upgrade();
            managedFactory.ApplyStats(managedFactory.FactoryData);

            _factoryTransform.GetComponent<UsineBehaviour>().SetUpgradeMesh(_factoryData.CurrentUpgrade.UpgradePrefab);

            if (_factoryData.CurrentUpgrade.NextUpgrade == null)
            {
                upgradeButton.SetActive(false);
            }
        }

    }

    public void EmptyUsine()
    {
        _factoryData.SetProjectileAmmount(0);
    }

    public void DestroyUsine()
    {
        LevelReferences.Instance.Player.GetComponent<GoldManager>().CollectMoney(_factoryData.SellPrice - (_factoryData.SellPrice / 3));
        Destroy(_factoryTransform.gameObject);
        gameObject.SetActive(false);
    }

    public void CreateInfoPanel()
    {
        UIManager uiManager = LevelReferences.Instance.Player.GetComponent<UIManager>();

        uiManager.CreateFactoryUpgradeInformation(factory.GetComponent<FactoryManager>().FactoryData);
    }
}
