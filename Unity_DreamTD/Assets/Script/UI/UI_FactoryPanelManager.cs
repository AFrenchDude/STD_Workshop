using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_FactoryPanelManager : MonoBehaviour
{
    private bool _canUpgrade;

    [SerializeField]
    private FactoryDatas _factoryDatas;

    [SerializeField]
    private Color _lowColor;
    [SerializeField]
    private Color _equalColor;
    [SerializeField]
    private Color _upColor;

    [Header("References")]
    [Space(16)]

    [Header("Header")]
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private Image _towerIcon;

    [Header("MaxAmmount")]
    [SerializeField]
    private TextMeshProUGUI _currentMaxAmmount;
    [SerializeField]
    private TextMeshProUGUI _upgradeMaxAmmount;

    [Header("ProductionRate")]
    [SerializeField]
    private TextMeshProUGUI _currentProductionRate;
    [SerializeField]
    private TextMeshProUGUI _upgradeProductionRate;

    [Header("Price")]
    [SerializeField]
    private TextMeshProUGUI _price;

    [Space(10)]

    [Header("Personaization")]

    [SerializeField]
    private Color _canBuyBackColor;
    [SerializeField]
    private Color _cantBuyBackColor;

    [SerializeField]
    private Image _backgroundImage;

    //Functions

    public void SetFactoryData(FactoryDatas factoryData)
    {
        _factoryDatas = factoryData;

        if (_factoryDatas.CurrentUpgrade.NextUpgrade != null)
        {

            SetUpStat();
        }
    }

    public void CanUpgrade(bool canUpgrade)
    {
        _canUpgrade = canUpgrade;
        UpdateCanUpgrade();
    }

    private void SetUpStat()
    {
        _towerIcon.sprite = _factoryDatas.Icon;
        _name.text = _factoryDatas.CurrentUpgrade.UpgradeName;

        SetUpUpgrade(_currentMaxAmmount, _upgradeMaxAmmount, _factoryDatas.MaxAmmount, _factoryDatas.CurrentUpgrade.NextUpgrade.UpgradeMaxResource, false);
        SetUpUpgrade(_currentProductionRate, _upgradeProductionRate, _factoryDatas.ProductionRate, _factoryDatas.CurrentUpgrade.NextUpgrade.UpgradeCooldown, true);

        _price.text = _factoryDatas.CurrentUpgrade.UpgradePrice.ToString();

        UpdateCanUpgrade();
    }

    private void UpdateCanUpgrade()
    {
        if (_canUpgrade)
        {
            _backgroundImage.color = _canBuyBackColor;
            _price.color = _upColor;
        }
        else
        {
            _backgroundImage.color = _cantBuyBackColor;
            _price.color = _lowColor;
        }
    }

    private void SetUpUpgrade(TextMeshProUGUI previous, TextMeshProUGUI next, float previousValue, float nextValue, bool invert)
    {
        previous.text = previousValue.ToString();
        next.text = nextValue.ToString();

        if (previousValue < nextValue)
        {
            if (invert)
            {
                next.color = _lowColor;
                previous.color = _upColor;
            }
            else
            {

                previous.color = _lowColor;
                next.color = _upColor;
            }
        }
        else if (previousValue > nextValue)
        {
            if (invert)
            {
                previous.color = _lowColor;
                next.color = _upColor;
            }
            else
            {
                next.color = _lowColor;
                previous.color = _upColor;

            }
        }
        else
        {
            previous.color = _equalColor;
            next.color = _equalColor;
        }
    }

    private bool _isFadeOut = false;
    public void FadeOut()
    {
        _isFadeOut = true;
        if (this != null) // Bah enfaite le boulanger ma dit qu'il n'existait pas...
        {
            if (transform.TryGetComponent(out Animator animator))
            {

                GetComponent<Animator>().SetBool("Close", _isFadeOut);
            }

        }
    }

    public void DestroySelf()
    {
        if (_isFadeOut)
        {
            Destroy(this.gameObject);
        }
    }


}
