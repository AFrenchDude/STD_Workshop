using System;
using TMPro;
using UnityEngine;

public class ProductionValueTextHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public float productionValue;
    public UsineBehaviour usineBehaviour;

    private void Update()
    {
        text.SetText("Production: " + productionValue);
    }

    public void OnProductionValueChange(Single newValue)
    {
        productionValue = newValue * usineBehaviour.maxRessource;
    }
}
