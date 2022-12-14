//From Template
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsineSlot : MonoBehaviour
{
    [SerializeField] private UsineDescription usineDescription = null;

    [SerializeField] private Button _button = null;

    [SerializeField] private Image _icon = null;

    [SerializeField] private TextMeshProUGUI _priceTxt = null;

    public UsineDescription UsineDescription => usineDescription;

    public delegate void UsineSlotEvent(UsineSlot sender);
    public event UsineSlotEvent OnUsineSlotClicked = null;

    [SerializeField]
    private InfoFactoryManager infoFactoryManager;

    private void Awake()
    {
        UpdateSlot();
    }

    [ContextMenu("Update Slot")]
    public void UpdateSlot()
    {
        if (usineDescription == null)
        {
            Debug.LogErrorFormat("{0}.UpdateSlot() Missing _towerDescription reference in {1}.", GetType().Name, name);
            return;
        }

        _icon.sprite = usineDescription.Icon;
        _icon.color = usineDescription.IconColor;
        _priceTxt.SetText(usineDescription.Price.ToString());
        infoFactoryManager.MoneyNecessary.text = _priceTxt.text;
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
        OnUsineSlotClicked?.Invoke(this);
    }
}
