using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoTowerManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private InfoTower info = null;

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
        sprite.sprite = towerDescription.Icon;
        name.text = info.name;
        type.text = info.type;
        Shoot.text = "Firerate : " + info.TowersDatas.FireRate + "/s";
        capacity.text = "Storage : " + info.TowersDatas.MaxProjectilesAmmount;
        moneySprite.sprite = info.moneySprite;
        moneyNecessary.text = " " + towerDescription.Price + "G";
    }
}
