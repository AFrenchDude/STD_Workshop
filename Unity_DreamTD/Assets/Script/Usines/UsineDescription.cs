//From Template
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Usine Description", fileName = "UsineDescription")]
public class UsineDescription : ScriptableObject
{
    [SerializeField]
    private UsineBehaviour _prefab = null;

    [SerializeField]
    private Sprite _icon = null;

    [SerializeField]
    private Color _iconColor = Color.white;

    [SerializeField]
    private int _price =0;

    public UsineBehaviour Prefab => _prefab;
    public Sprite Icon => _icon;
    public Color IconColor => _iconColor;
    public int Price => _price;

    public UsineBehaviour Instantiate()
    {
        _prefab.getFactoryData.UnlockFactoryrData();
        UsineBehaviour spawnedUsine = Instantiate(_prefab);
        spawnedUsine.transform.GetComponent<HUDwhenSelect>().hudRef = LevelReferences.Instance.Player.GetComponent<Selector>().usineHUD;
        spawnedUsine.SetPrice(_price);
        return spawnedUsine;
    }
}