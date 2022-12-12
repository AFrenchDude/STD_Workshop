// By DIJOUX Kevin
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoFactoryManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private InfoFactory factoryDescription = null;

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
    private TextMeshProUGUI moneyNecessary = null;
    #endregion Variables

    private void Start()
    {
        sprite.sprite = factoryDescription.FactoryDatas.Icon;
        name.text = factoryDescription.FactoryDatas.Name;
        type.text = factoryDescription.FactoryDatas.Type;
        production.text = factoryDescription.FactoryDatas.ProductionRate + "/s";
        capacity.text = factoryDescription.FactoryDatas.MaxAmmount.ToString();
        moneyNecessary.text = factoryDescription.FactoryDatas.SellPrice + " ";
    }
}
