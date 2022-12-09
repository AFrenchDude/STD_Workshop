using TMPro;
using UnityEngine;
using UnityEngine.UI;

    //By DIJOUX Kevin
public class WagonHUD : MonoBehaviour
{
    [Header("Panel components")]
    [SerializeField] private Image _icon = null;
    [SerializeField] private TextMeshProUGUI _currentStorageValue;
    [SerializeField] private TextMeshProUGUI _maxStoragevalue;
    [SerializeField] private Slider _currentStorageValueSlider;

    public Image Icon => _icon;
    public TextMeshProUGUI CurrentStorageValue => _currentStorageValue;
    public TextMeshProUGUI MaxStorage => _maxStoragevalue;
    public Slider CurrentStorageValueSlider => _currentStorageValueSlider;
}
