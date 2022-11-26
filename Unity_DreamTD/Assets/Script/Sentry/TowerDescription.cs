//From Template
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Tower Description", fileName = "TowerDescription")]
public class TowerDescription : ScriptableObject
{
    [SerializeField]
    private string _name = null;

    [SerializeField]
    private Sentry _prefab = null;

    [SerializeField]
    private Sprite _icon = null;

    [SerializeField]
    private Color _iconColor = Color.white;

    [SerializeField]
    private int _price =0;

    [SerializeField]
    private TowersDatas _towersDatas;

    public string Name => _name;
    public Sentry Prefab => _prefab;
    public Sprite Icon => _icon;
    public Color IconColor => _iconColor;
    public int Price => _price;
    public TowersDatas TowersDatas => _towersDatas; 

    public Sentry Instantiate()
    {
        Sentry spawnedSentry = Instantiate(_prefab);
        spawnedSentry.SetPrice(_price);
        return spawnedSentry;
    }
}