using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class TowerHUD : MonoBehaviour
{
    public TowerGetProjectile tower;
    [SerializeField] private GameObject changeTypeHUD;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image productionIcon;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Dropdown dropdown;
    [HideInInspector] public float productionValue;

    private float currentResources;
    private float maxResources;

    private void Update()
    {
        text.SetText("Production: " + productionValue);
        if (tower != null)
        {
            currentResources = tower.projectiles;
            maxResources = tower.maxRessource;
            slider.value = currentResources / maxResources;

        }
    }

    // PLEASE REMY PUT THESE TWO TWIN METHOD IN THE CORRECT PLACE WHEN OPENING CLOSING THE UI, HAVING MULTIPLE TOWER CAUSE PROBLEMS
    private void OnEnable()
    {
            Tower towerRef = tower.transform.parent.GetComponent<Tower>();
            if (towerRef != null)
            {
                towerRef.RangeIndicator.EnableRangeIndicator(true);
            }
    }
    // PLEASE REMY PUT THESE TWO TWIN METHOD IN THE CORRECT PLACE WHEN OPENING CLOSING THE UI, HAVING MULTIPLE TOWER CAUSE PROBLEMS
    private void OnDisable()
    {
        Tower towerRef = tower.transform.parent.GetComponent<Tower>();
        if (towerRef != null)
        {
            towerRef.RangeIndicator.EnableRangeIndicator(false);
        }
    }
    // PLEASE REMY PUT THESE TWO TWIN METHOD IN THE CORRECT PLACE WHEN OPENING CLOSING THE UI, HAVING MULTIPLE TOWER CAUSE PROBLEMS

    public void OnProductionValueChange(Single newValue)
    {
        productionValue = newValue * tower.maxRessource;
    }

    public void OnPick()
    {
        productionIcon.sprite = tower.type.icon;
        toggle.isOn = tower.transform.parent.GetComponent<WeaponController>().canShoot;
        changeTypeHUD.GetComponent<ChangeType>().openHUD = gameObject;
        dropdown.value = (int)tower.transform.parent.GetComponent<Tower>()._targetPriority;
        //Tower towerRef = tower.transform.parent.GetComponent<Tower>();
        //if (towerRef != null)
        //{
        //    towerRef.RangeIndicator.EnableRangeIndicator(true);
        //}
    }

    public void ChangeTowerBehaviour(Int32 newBehaviour)
    {
        tower.transform.parent.GetComponent<Tower>()._targetPriority = (TargetPriority)newBehaviour;
    }

    public void SetOnOff(bool isOn)
    {
        tower.transform.parent.GetComponent<WeaponController>().canShoot = isOn;
    }

    public void EmptyTower()
    {
        tower.projectiles = 0;
    }

    public void DestroyTower()
    {
        Destroy(tower.transform.parent.gameObject);
        gameObject.SetActive(false);
    }

    public void ChangeType()
    {
        changeTypeHUD.GetComponent<ChangeType>().objectToChange = tower.gameObject;
        changeTypeHUD.SetActive(true);
    }
}
