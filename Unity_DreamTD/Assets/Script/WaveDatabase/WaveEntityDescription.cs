//From Template
using UnityEngine;

[System.Serializable]
public class WaveEntityDescription
{
    [SerializeField]
    private NightmareData _nightmareData = null;

    [SerializeField]
    private float _extraDurationAfterSpawned = 0;

    [Range(1, 3)]
    [SerializeField]
    private int _spawningLane;

    public NightmareData NightmareData
    {
        get
        {
            return _nightmareData;
        }
    }

    public float ExtraDurationAfterSpawned
    {
        get
        {
            return _extraDurationAfterSpawned;
        }
    }

    public int SpawningLane
    {
        get
        {
            return _spawningLane;
        }
    }
}