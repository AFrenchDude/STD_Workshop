//From Template
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDatabaseManager : Singleton<WaveDatabaseManager>
{
    [SerializeField] private WaveDatabase _waveDatabase = null;

    public WaveDatabase WaveDatabase
    {
        get
        {
            return _waveDatabase;
        }
    }
}
