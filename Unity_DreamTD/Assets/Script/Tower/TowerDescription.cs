//From Template
using TMPro;
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

    [SerializeField]
    private Sprite _sprite = null;

    [SerializeField]
    private string _name = null;

    [SerializeField]
    private string _type = null;

    [SerializeField]
    private Sprite _moneySprite = null;
    public Sprite Icon => _sprite;
    public string Name => _name;
    public string Type => _type;


    public Sprite IconMoneySell => _moneySprite;
    public Tower Prefab => _prefab;
    public Color IconColor => _iconColor;
    public int Price => _price;

    [SerializeField]
    private TowersDatas _towersDatas;

    public TowersDatas TowersDatas => _towersDatas;

    public Tower Instantiate()
    {
        _towersDatas.UnlockTowerData();
        Tower spawnedSentry = Instantiate(_prefab);
        spawnedSentry.transform.GetComponent<HUDwhenSelect>().hudRef = LevelReferences.Instance.Player.GetComponent<Selector>().towerHUD;
        spawnedSentry.SetPrice(_price);
        spawnedSentry.SetTowerDescription(this);
        return spawnedSentry;
    }

    private TowerSlot _slotReference;
    public void SetSlotRef(TowerSlot slot)
    {
        _slotReference = slot;
    }

    public void IncreasePrice()
    {
        _price = Mathf.RoundToInt(_price + 20);
        _slotReference.UpdateSlot();
    }
}