//From Template
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Tower Description", fileName = "TowerDescription")]
public class TowerDescription : ScriptableObject
{
    [SerializeField]
    private Tower _prefab = null;

    [SerializeField]
    private Sprite _icon = null;

    [SerializeField]
    private Color _iconColor = Color.white;

    [SerializeField]
    private int _price =0;

    public Tower Prefab => _prefab;
    public Sprite Icon => _icon;
    public Color IconColor => _iconColor;
    public int Price => _price;

    public Tower Instantiate()
    {
        Tower spawnedSentry = Instantiate(_prefab);
        spawnedSentry.transform.GetComponent<HUDwhenSelect>().hudRef = LevelReferences.Instance.Player.GetComponent<Selector>().towerHUD;
        spawnedSentry.SetPrice(_price);
        return spawnedSentry;
    }
}