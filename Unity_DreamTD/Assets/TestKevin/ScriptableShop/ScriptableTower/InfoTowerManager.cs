using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InfoTowerManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private TowerDescription towerDescription = null;

    [SerializeField]
    private Image sprite = null;

    [SerializeField]
    private new TextMeshProUGUI name = null;

    [SerializeField]
    private TextMeshProUGUI type = null;

    [SerializeField]
    private TextMeshProUGUI Shoot = null;

    [SerializeField]
    private TextMeshProUGUI capacity = null;

    [SerializeField]
    private Image moneySprite = null;

    [SerializeField]
    private TextMeshProUGUI moneyNecessary = null;
    #endregion Variables

    private void Start()
    {
        sprite.sprite = towerDescription.TowersDatas.Icon;
        name.text = towerDescription.TowersDatas.Name;
        type.text = towerDescription.TowersDatas.Type;
        Shoot.text = "Firerate : " + towerDescription.TowersDatas.FireRate + "/s";
        capacity.text = "Storage : " + towerDescription.TowersDatas.MaxProjectilesAmmount;
        moneySprite.sprite = towerDescription.IconMoneySell;
        moneyNecessary.text = " " + towerDescription.Price + " G ";
    }
}
