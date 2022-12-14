//By ALEXANDRE Dorian
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/NightmareData", fileName = "NightmareData")]
public class NightmareData : ScriptableObject
{
    public enum NighmareType
    {
        Vegetables,
        Skeleton,
        Insects,
        Neutral
    }

    public enum NightmareFunction
    {
        Basic,
        Support,
        Fly
    }
    public enum Weakness
    {
        Pizza,
        Stars,
        Gum
    }

    [SerializeField]
    private NighmareType _nighmareType;

    [SerializeField]
    private NightmareFunction _nightmareFunction;

    [SerializeField]
    private Weakness _weakness;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private int _rewardGold;

    [SerializeField]
    private float _maxLife;

    [SerializeField]
    private float _addedBoost;

    [SerializeField]
    private Color _debugColor;

    [Header("UI")]
    [SerializeField]
    private Sprite _icon;

    private int _killCount = 0;



    public NighmareType nighmareType => _nighmareType;
    public NightmareFunction nightmareFunction => _nightmareFunction;
    public Weakness weakness => _weakness;
    public float speed => _speed;
    public int rewardGold => _rewardGold;
    public float maxLife => _maxLife;
    public float Boost => _addedBoost;
    public Color debugColor => _debugColor;
    public int KillCount => _killCount;

    public Sprite icon => _icon;

    public NighmareType getNightmareType
    {
        get { return _nighmareType; }
    }

    public void ResetKillCount()
    {
        _killCount = 0;
    }

    public void AddKillCount()
    {
        _killCount++;
    }
}
