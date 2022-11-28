using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private void Update()
    {
        text.SetText("Production: " + productionValue);
        if (usineBehaviour != null)
        {
            currentResources = usineBehaviour.projectiles.Count;
            maxResources = usineBehaviour.maxRessource;
            slider.value = currentResources / maxResources;
        }
    }

    public void OnProductionValueChange(Single newValue)
    {
        productionValue = newValue * usineBehaviour.maxRessource;
    }

    public void OnPick()
    {
        productionIcon.sprite = usineBehaviour.type.icon;
        toggle.isOn = usineBehaviour.isProducing;
    }

    public void SetOnOff(bool isOn)
    {
        usineBehaviour.isProducing = isOn;
    }

    public void EmptyUsine()
    {
        usineBehaviour.projectiles.Clear();
    }

    public void DestroyUsine()
    {
        Destroy(usineBehaviour.transform.gameObject);
        gameObject.SetActive(false);
    }
}
