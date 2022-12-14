// By DIJOUX Kevin
using UnityEngine;

[CreateAssetMenu(menuName = "Info UI/InfoFactoryButtonShop", fileName = "InfoFactoryButtonShop")]
public class InfoFactory : ScriptableObject
{
    [SerializeField] public Sprite sprite = null;
    [SerializeField] public new string name = null;
    [SerializeField] public string type = null;
    [SerializeField] public Sprite moneySprite = null;

    [SerializeField]
    private FactoryDatas _factoryDatas;

    public FactoryDatas FactoryDatas => _factoryDatas;
}
