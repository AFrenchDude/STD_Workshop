//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReferences : Singleton<LevelReferences>
{
    [SerializeField] SpawnerManager _spawnerManager = null;


    public SpawnerManager SpawnerManager => _spawnerManager;
}
