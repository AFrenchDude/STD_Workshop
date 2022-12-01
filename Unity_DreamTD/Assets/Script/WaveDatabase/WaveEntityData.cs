//From Template
using UnityEngine;

[System.Serializable]
public class WaveEntityData
{
    [SerializeField]
    private WaveEntity _waveEntityPrefab = null;

    [SerializeField]
    private NightmareData.NighmareType _nightmareType = default;

    [SerializeField]
    private NightmareData.NightmareFunction _nightmareFunction = default;

    public WaveEntity WaveEntityPrefab => _waveEntityPrefab;
    public NightmareData.NighmareType NightmareType => _nightmareType;
    public NightmareData.NightmareFunction NightmareFunction => _nightmareFunction;

}