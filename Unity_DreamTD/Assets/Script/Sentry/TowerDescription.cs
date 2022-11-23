//From Template
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Tower Description", fileName = "TowerDescription")]
public class TowerDescription : ScriptableObject
{
    [SerializeField]
    private Sentry _prefab = null;

    [SerializeField]
    private Sprite _icon = null;

    [SerializeField]
    private Color _iconColor = Color.white;

    public Sentry Prefab => _prefab;
    public Sprite Icon => _icon;
    public Color IconColor => _iconColor;

    public Sentry Instantiate()
    {
        return GameObject.Instantiate(_prefab);
    }
}