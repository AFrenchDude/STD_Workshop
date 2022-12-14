//From Template
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TowerSlot : MonoBehaviour
{
    [SerializeField]
    private TowerHUD towerHUD = null;

    [SerializeField]
    private TowerDescription _towerDescription = null;

    [SerializeField]
    private Button _button = null;

    [SerializeField]
    private Image _icon = null;

    [SerializeField]
    private TextMeshProUGUI _priceTxt = null;

    public TowerDescription TowerDescription => _towerDescription;

    public delegate void TowerSlotEvent(TowerSlot sender);
    public event TowerSlotEvent OnTowerSlotClicked = null;

    [SerializeField]
    private InfoTowerManager infoTowerManager = null;

    private void Awake()
    {
        _towerDescription = Instantiate(_towerDescription);
        _towerDescription.SetSlotRef(this);
        UpdateSlot();
    }

    [ContextMenu("Update Slot")]
    public void UpdateSlot()
    {
        if (_towerDescription == null)
        {
            Debug.LogErrorFormat("{0}.UpdateSlot() Missing _towerDescription reference in {1}.", GetType().Name, name);
            return;
        }

        _icon.sprite = _towerDescription.Icon;
        _icon.color = _towerDescription.IconColor;
        _priceTxt.SetText(_towerDescription.Price.ToString());
        infoTowerManager.MoneyNecessary.text = _priceTxt.text;
    }

    private void OnEnable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        LevelReferences.Instance.Player.GetComponentInChildren<HUDAnimationController>().HUDswaper();
        OnTowerSlotClicked?.Invoke(this);
    }
}