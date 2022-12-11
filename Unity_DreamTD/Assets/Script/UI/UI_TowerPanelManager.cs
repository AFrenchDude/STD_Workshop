using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_TowerPanelManager : MonoBehaviour
{
    private bool _canUpgrade;

    [SerializeField]
    private TowersDatas _towerDatas;

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

    [Header("Damage")]
    [SerializeField]
    private TextMeshProUGUI _currentDamage;
    [SerializeField]
    private TextMeshProUGUI _upgradeDamage;

    [Header("FireRate")]
    [SerializeField]
    private TextMeshProUGUI _currentFireRate;
    [SerializeField]
    private TextMeshProUGUI _upgradeFireRate;

    [Header("Range")]
    [SerializeField]
    private TextMeshProUGUI _currentRange;
    [SerializeField]
    private TextMeshProUGUI _upgradeRange;

    [Header("Capacity")]
    [SerializeField]
    private TextMeshProUGUI _currentCapacity;
    [SerializeField]
    private TextMeshProUGUI _upgradeCapacity;

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

    public void SetTowerData(TowersDatas towerData)
    {
        _towerDatas = towerData;


        SetUpStat();
    }

    public void CanUpgrade(bool canUpgrade)
    {
        _canUpgrade = canUpgrade;
    }

    private void SetUpStat()
    {
        if (_towerDatas.canUpgrade)
        {
            _towerIcon.sprite = _towerDatas.Icon;
            _name.text = _towerDatas.UpgradeDatas.UpgradeName; 

            SetUpUpgrade(_currentDamage, _upgradeDamage, _towerDatas.Damage, _towerDatas.UpgradeDatas.NextUpgrade.UpgradeDamage, false);
            SetUpUpgrade(_currentFireRate, _upgradeFireRate, _towerDatas.FireRate, _towerDatas.UpgradeDatas.NextUpgrade.UpgradeFireRate, true);
            SetUpUpgrade(_currentRange, _upgradeRange, _towerDatas.Range, _towerDatas.UpgradeDatas.NextUpgrade.UpgradeRange, false);
            SetUpUpgrade(_currentCapacity, _upgradeCapacity, _towerDatas.MaxProjectilesAmmount, _towerDatas.UpgradeDatas.NextUpgrade.UpgradeMaxProjectiles, false);

            _price.text = _towerDatas.UpgradeDatas.UpgradePrice.ToString();

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
