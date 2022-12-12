using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoCurrentFactory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name = null;
    [SerializeField] private TextMeshProUGUI _damage = null;
    [SerializeField] private TextMeshProUGUI _maxStorage = null;

    public TextMeshProUGUI Name => _name;
    public TextMeshProUGUI Damage => _damage;
    public TextMeshProUGUI MaxStorage => _maxStorage;

    public void Upgrade(FactoryDatas datas)
    {
        _name.text = datas.Name.ToString();
    }
}
