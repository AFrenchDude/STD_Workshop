using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WagonContentPreview : MonoBehaviour
{
    private Wagon _associatedWagon;
    private int _containedProjectiles;
    private int _maxStorage;

    [Header("References")]
    [SerializeField]
    private Image _backgroundImage;

    [SerializeField]
    private Image _iconImage;

    [SerializeField]
    private TextMeshProUGUI _currentAmmount;
    [SerializeField]
    private TextMeshProUGUI _maxAmmount;

    [SerializeField]
    private Slider _sliderAmmount;

    [Header("Fixed Datas")]
    [SerializeField]
    private Color _minimalAmmountColor;
    [SerializeField]
    private Color _maxAmmountColor;

    public void SetWagonContent(Wagon wagon)
    {
        _associatedWagon = wagon;
        UpdateWagonData();
    }

    public void UpdateWagonData()
    {
        _backgroundImage.sprite = _associatedWagon.type.PrjectileBackgroundSprite;

        _iconImage.sprite = _associatedWagon.type.icon;

       
        _maxStorage = _associatedWagon.MaxWagonStorage;

        UpdateWagonContent(_associatedWagon.projectiles);
    }

    private void UpdateWagonContent(int content)
    {
        _containedProjectiles = content;

        _currentAmmount.text = _containedProjectiles.ToString();
        _sliderAmmount.value = _containedProjectiles / _maxStorage;
        _sliderAmmount.GetComponent<Image>().color = Color.Lerp(_minimalAmmountColor, _maxAmmountColor, _containedProjectiles / _maxStorage);
    }
}
