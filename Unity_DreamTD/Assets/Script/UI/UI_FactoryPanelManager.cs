using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UI_FactoryPanelManager : MonoBehaviour
{

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

    //Functions

    public void SetFactoryData(FactoryDatas factoryData)
    {
        _factoryDatas = factoryData;

        if (_factoryDatas.CurrentUpgrade.NextUpgrade != null)
        {

            SetUpStat();
        }
    }

    private void SetUpStat()
    {
        _towerIcon.sprite = _factoryDatas.Icon;
        _name.text = _factoryDatas.CurrentUpgrade.UpgradeName;

        SetUpUpgrade(_currentMaxAmmount, _upgradeMaxAmmount, _factoryDatas.MaxAmmount, _factoryDatas.CurrentUpgrade.NextUpgrade.UpgradeMaxResource, false);
        SetUpUpgrade(_currentProductionRate, _upgradeProductionRate, _factoryDatas.ProductionRate, _factoryDatas.CurrentUpgrade.NextUpgrade.UpgradeCooldown, true);

        _price.text = _factoryDatas.CurrentUpgrade.UpgradePrice.ToString();
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
        if (GetComponent<Animator>() != null)
        {

            GetComponent<Animator>().SetBool("Close", _isFadeOut);
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
