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

    [Header("Locker")]
    [SerializeField] private bool _isUnlock = true;

    [SerializeField] private Sprite _lockSprite;

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

        if (_isUnlock)
        {
            _icon.sprite = usineDescription.Icon;
            _icon.color = usineDescription.IconColor;
            _priceTxt.SetText(usineDescription.Price.ToString());
            infoFactoryManager.MoneyNecessary.text = _priceTxt.text;
        }
        else
        {
            _icon.sprite = _lockSprite;
            //_icon.color = usineDescription.IconColor - new Color(0.5f,0.5f,.5f,0f);
            _priceTxt.SetText("");
            //infoFactoryManager.MoneyNecessary.text = _priceTxt.text;
        }
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
        if (_isUnlock)
        {
            LevelReferences.Instance.Player.GetComponentInChildren<HUDAnimationController>().HUDswaper();
            OnUsineSlotClicked?.Invoke(this);
        }
    }
}
