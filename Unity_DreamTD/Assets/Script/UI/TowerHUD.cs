using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class TowerHUD : MonoBehaviour
{
    public GameObject tower;
    [SerializeField] private GameObject changeTypeHUD;
    [SerializeField] private GameObject ChangeTypeButton2;
    [SerializeField] private TowersDatas doubleCanonRef;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private Slider slider2;
    [SerializeField] private Image productionIcon;
    [SerializeField] private Image productionIcon2;
    [SerializeField] private Toggle toggle;
    [SerializeField] private TMP_Dropdown dropdown;

    [HideInInspector] public Tower towerScriptRef;
    private TowerGetProjectile towerGetProjectileScriptRef;
    [HideInInspector] public int productionValue;
    private float currentResources;
    private float maxResources;

    //Set resources text and slider
    private void Update()
    {
        currentResources = tower.GetComponent<TowerManager>().TowersData.Projectiles[0].ProjectileAmmount;
        text.SetText("Bullet: " + currentResources);

        if(tower.GetComponent<TowerManager>().TowersData.Projectiles.Count > 1)
        {
            float secondFireProjectiles = tower.GetComponent<TowerManager>().TowersData.Projectiles[1].ProjectileAmmount;
            text2.SetText("Bullet: " + secondFireProjectiles);
            slider2.value = secondFireProjectiles / tower.GetComponent<TowerManager>().TowersData.Projectiles[1].MaxProjectilesAmmount;
        }

        if (tower != null && towerGetProjectileScriptRef != null)
        {
            maxResources = tower.GetComponent<TowerManager>().TowersData.MaxProjectilesAmmount;
            slider.value = (currentResources / maxResources);
        }
        if (gameObject.activeSelf)
        {
            towerScriptRef.RangeIndicator.EnableRangeIndicator(true);
        }
    }

    public void OnPick(GameObject towerClicked)
    {
        tower = towerClicked;
        towerScriptRef = tower.GetComponent<Tower>();
        towerGetProjectileScriptRef = tower.GetComponentInChildren<TowerGetProjectile>();
        productionIcon.sprite = towerGetProjectileScriptRef.type[0].icon;
        toggle.isOn = tower.GetComponent<WeaponController>().canShoot;
        changeTypeHUD.GetComponent<ChangeType>().openHUD = gameObject;
        if (tower.GetComponent<TowerManager>().TowersData.FireType == doubleCanonRef.FireType)
        {
            productionIcon2.sprite = towerGetProjectileScriptRef.type[1].icon;
            ChangeTypeButton2.SetActive(true);
            slider2.gameObject.SetActive(true);
            text2.gameObject.SetActive(true);
        }
        else
        {
            ChangeTypeButton2.SetActive(false);
            slider2.gameObject.SetActive(false);
            text2.gameObject.SetActive(false);
        }
        dropdown.value = (int)towerScriptRef._targetPriority;
        towerScriptRef.RangeIndicator.EnableRangeIndicator(false);

        if (tower.GetComponent<TowerManager>().TowersData.canUpgrade)
        {
            upgradeButton.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(false);
        }
    }

    public void OnUnpick()
    {
        towerScriptRef.RangeIndicator.EnableRangeIndicator(false);
        towerScriptRef = null;
        towerGetProjectileScriptRef = null;
        tower = null;
    }

    public void ChangeTowerBehaviour(Int32 newBehaviour)
    {
        towerScriptRef._targetPriority = (TargetPriority)newBehaviour;
    }

    public void SetOnOff(bool isOn)
    {
        tower.GetComponent<WeaponController>().canShoot = isOn;
    }

    public void Upgrade()
    {
        //tower.GetComponent<TowerManager>().TowersData.Upgrade();
        TowerManager managedTower = tower.GetComponent<TowerManager>();
        managedTower.TowersData.Upgrade();
        managedTower.ApplyStats(managedTower.TowersData);
        towerScriptRef.RangeIndicator.UpdateCircle();
        OnPick(tower);
    }

    public void EmptyTower()
    {
        towerGetProjectileScriptRef.projectiles = 0;
    }

    public void ChangeType(int intCanonRef)
    {
        changeTypeHUD.GetComponent<ChangeType>().objectToChange = tower;
        changeTypeHUD.GetComponent<ChangeType>().canonToChange = intCanonRef;
        changeTypeHUD.SetActive(true);
        changeTypeHUD.GetComponent<ChangeType>().noTypeButton.SetActive(true);
    }

    public void DestroyTower()
    {
        Base.Instance.AddGold(towerScriptRef.Price - (towerScriptRef.Price / 3));
        Destroy(tower);
        gameObject.SetActive(false);
    }
}
