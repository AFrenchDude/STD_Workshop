// By DIJOUX Kevin
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoFactoryManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private InfoFactory info = null;

    [SerializeField]
    private UsineDescription factoryDescription = null;

    [SerializeField]
    private Image sprite = null;

    [SerializeField]
    private new TextMeshProUGUI name = null;

    [SerializeField]
    private TextMeshProUGUI type = null;

    [SerializeField]
    private TextMeshProUGUI production = null;

    [SerializeField]
    private TextMeshProUGUI capacity = null;

    [SerializeField]
    private Image moneySprite = null;

    [SerializeField]
    private TextMeshProUGUI moneyNecessary = null;
    #endregion Variables

    private void Start()
    {
        sprite.sprite = factoryDescription.Icon;
        name.text = info.name;
        type.text = info.type;
        production.text = "Production : " + info.FactoryDatas.ProductionRate + "/s";
        capacity.text = "Storage : " + info.FactoryDatas.MaxAmmount;
        moneySprite.sprite = info.moneySprite;
        moneyNecessary.text = " " + info.FactoryDatas.SellPrice + " G ";
    }
}
