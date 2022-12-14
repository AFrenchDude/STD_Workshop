// By DIJOUX Kevin
using UnityEngine;

[CreateAssetMenu(menuName = "Info UI/InfoTowerButtonShop", fileName = "InfoTowerButtonShop")]
public class InfoTower : ScriptableObject
{
    [SerializeField] public Sprite sprite = null;
    [SerializeField] public new string name = null;
    [SerializeField] public string type = null;
    [SerializeField] public Sprite moneySprite = null;

    [SerializeField]
    private TowersDatas _towersDatas;

    public TowersDatas TowersDatas => _towersDatas;
}