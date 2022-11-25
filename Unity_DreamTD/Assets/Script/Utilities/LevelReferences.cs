//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReferences : Singleton<LevelReferences>
{
    [SerializeField] SpawnerManager _spawnerManager = null;
    [SerializeField] PlayerDrag _playerDrag = null;
    [SerializeField] SplineDone _railSpline = null;


    public SpawnerManager SpawnerManager => _spawnerManager;
    public PlayerDrag PlayerDrag => _playerDrag;
    public SplineDone RailSpline => _railSpline;
}
