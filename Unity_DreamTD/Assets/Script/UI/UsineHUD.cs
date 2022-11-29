using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class UsineHUD : MonoBehaviour
{
    public UsineBehaviour usineBehaviour;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image productionIcon;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider slider;
    [HideInInspector] public float productionValue;

    private float currentResources;
    private float maxResources;

    //Set resources text and slider
    private void Update()
    {
        text.SetText("Production: " + productionValue);
        if (usineBehaviour != null)
        {
            currentResources = usineBehaviour.projectiles;
            maxResources = usineBehaviour.maxRessource;
            slider.value = currentResources / maxResources;
        }
    }

    public void OnProductionValueChange(Single newValue)
    {
        productionValue = newValue * usineBehaviour.maxRessource;
    }

    public void OnPick(UsineBehaviour pickedUsine)
    {
        usineBehaviour = pickedUsine;
        productionIcon.sprite = usineBehaviour.type.icon;
        toggle.isOn = usineBehaviour.isProducing;
    }

    public void OnUnpick()
    {
        usineBehaviour = null;
    }

    public void SetOnOff(bool isOn)
    {
        usineBehaviour.isProducing = isOn;
    }

    public void Upgrade()
    {

    }

    public void EmptyUsine()
    {
        usineBehaviour.projectiles = 0;
    }

    public void DestroyUsine()
    {
        Base.Instance.AddGold(usineBehaviour.Price - (usineBehaviour.Price / 3));
        Destroy(usineBehaviour.transform.gameObject);
        gameObject.SetActive(false);
    }
}
