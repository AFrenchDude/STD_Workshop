using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class TowerHUD : MonoBehaviour
{
    public GameObject tower;
    [SerializeField] private GameObject changeTypeHUD;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image productionIcon;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Dropdown dropdown;

    [HideInInspector] public Tower towerScriptRef;
    private TowerGetProjectile towerGetProjectileScriptRef;
    [HideInInspector] public float productionValue;
    private float currentResources;
    private float maxResources;

    //Set resources text and slider
    private void Update()
    {
        text.SetText("Production: " + productionValue);
        if (tower != null && towerGetProjectileScriptRef != null)
        {
            currentResources = towerGetProjectileScriptRef.projectiles;
            maxResources = towerGetProjectileScriptRef.maxRessource;
            slider.value = currentResources / maxResources;
        }
        if (gameObject.activeSelf)
        {
            towerScriptRef.RangeIndicator.EnableRangeIndicator(true);
        }
    }

    public void OnProductionValueChange(Single newValue)
    {
        productionValue = newValue * towerGetProjectileScriptRef.maxRessource;
    }

    public void OnPick()
    {
        towerScriptRef = tower.GetComponent<Tower>();
        towerGetProjectileScriptRef = tower.GetComponentInChildren<TowerGetProjectile>();
        productionIcon.sprite = towerGetProjectileScriptRef.type.icon;
        toggle.isOn = tower.GetComponent<WeaponController>().canShoot;
        changeTypeHUD.GetComponent<ChangeType>().openHUD = gameObject;
        dropdown.value = (int)towerScriptRef._targetPriority;
        towerScriptRef.RangeIndicator.EnableRangeIndicator(false);
    }

    public void ChangeTowerBehaviour(Int32 newBehaviour)
    {
        towerScriptRef._targetPriority = (TargetPriority)newBehaviour;
    }

    public void SetOnOff(bool isOn)
    {
        tower.GetComponent<WeaponController>().canShoot = isOn;
    }

    public void EmptyTower()
    {
        towerGetProjectileScriptRef.projectiles = 0;
    }

    public void DestroyTower()
    {
        Destroy(tower);
        gameObject.SetActive(false);
    }

    public void ChangeType()
    {
        changeTypeHUD.GetComponent<ChangeType>().objectToChange = tower;
        changeTypeHUD.SetActive(true);
    }
}
