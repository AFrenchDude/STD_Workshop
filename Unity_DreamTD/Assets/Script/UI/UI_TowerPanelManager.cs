using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_TowerPanelManager : MonoBehaviour
{

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

    [Header("Price")]
    [SerializeField]
    private TextMeshProUGUI _price;

    //Functions

    public void SetTowerData(TowersDatas towerData)
    {
        _towerDatas = towerData;


        SetUpStat();
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

            _price.text = _towerDatas.UpgradeDatas.UpgradePrice.ToString();
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
