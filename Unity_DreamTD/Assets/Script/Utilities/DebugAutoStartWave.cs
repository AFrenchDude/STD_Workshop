//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAutoStartWave : MonoBehaviour
{
    private void Awake()
    {
        LevelReferences.Instance.SpawnerManager.StartWaves();
    }
}
