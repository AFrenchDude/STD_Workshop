using TMPro;
using UnityEngine;
using UnityEngine.UI;

    //By DIJOUX Kevin
public class WagonHUD : MonoBehaviour
{
    [Header("Panel components")]
    [SerializeField] private Image _icon = null;
    [SerializeField] private TextMeshProUGUI _currentStorageText;
    [SerializeField] private TextMeshProUGUI _maxStoragevalueText;
    [SerializeField] private Slider _currentStorageValueSlider;
    [SerializeField] private CurrentProjectileUI _currentProjectileUI;

    public Image Icon => _icon;
    public TextMeshProUGUI CurrentStorageText => _currentStorageText;
    public TextMeshProUGUI MaxStorageText => _maxStoragevalueText;
    public Slider CurrentStorageValueSlider => _currentStorageValueSlider;
    public CurrentProjectileUI CurrentProjectileUI => _currentProjectileUI;

    public void SetSliderValue(int currentValue, int maxValue)
    {
        _currentStorageValueSlider.value = currentValue;
        _currentStorageValueSlider.maxValue = maxValue;
    }

}
