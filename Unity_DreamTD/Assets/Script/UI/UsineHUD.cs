using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsineHUD : MonoBehaviour
{
    public UsineBehaviour usineBehaviour;
    [SerializeField] private Image productionIcon;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider slider;
    [SerializeField] private ProductionValueTextHUD productionValueTextHUD;

    private float currentResources;
    private float maxResources;

    private void Update()
    {
        if (usineBehaviour != null)
        {
            currentResources = usineBehaviour.projectiles.Count;
            maxResources = usineBehaviour.maxRessource;
            slider.value = currentResources / maxResources;
        }
    }

    public void OnPick()
    {
        productionValueTextHUD.usineBehaviour = usineBehaviour;
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
