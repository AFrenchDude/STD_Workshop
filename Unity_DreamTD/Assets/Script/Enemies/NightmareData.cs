//By ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private NighmareType _nighmareType;

    [SerializeField]
    private NightmareFunction _nightmareFunction;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private int _rewardGold;

    [SerializeField]
    private float _maxLife;

    [SerializeField]
    private Color _debugColor;

    [Header("UI")]
    [SerializeField]
    private Sprite _icon;

    private int _killCount = 0;



    public NighmareType nighmareType => _nighmareType;
    public NightmareFunction nightmareFunction => _nightmareFunction;
    public float speed => _speed;
    public int rewardGold => _rewardGold;
    public float maxLife => _maxLife;
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
