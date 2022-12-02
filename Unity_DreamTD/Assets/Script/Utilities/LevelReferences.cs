//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReferences : Singleton<LevelReferences>
{
    [SerializeField] SpawnerManager _spawnerManager = null;
    [SerializeField] PlayerDrag _playerDrag = null;
    [SerializeField] SplineDone _railSpline = null;
    [SerializeField] GameObject _player = null;
    [SerializeField] GameObject _station = null;
    [SerializeField] AudioSource _musicPlayer = null;


    public SpawnerManager SpawnerManager => _spawnerManager;
    public PlayerDrag PlayerDrag => _playerDrag;
    public SplineDone RailSpline => _railSpline;
    public GameObject Player => _player;
    public GameObject Station => _station;
    public AudioSource MusicPlayer => _musicPlayer;
}
