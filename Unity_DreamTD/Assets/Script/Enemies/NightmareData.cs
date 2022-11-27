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
        Insects
    }

    public enum NightmareFunction
    {
        Basic,
        Booster,
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


    public NighmareType nighmareType => _nighmareType;
    public NightmareFunction nightmareFunction => _nightmareFunction;
    public float speed => _speed;
    public int rewardGold => _rewardGold;
    public float maxLife => _maxLife;
    public Color debugColor => _debugColor;
}
