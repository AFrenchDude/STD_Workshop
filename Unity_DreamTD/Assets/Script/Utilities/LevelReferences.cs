//By ALBERT Esteban
using UnityEngine;

public class LevelReferences : Singleton<LevelReferences>
{
    [SerializeField] Camera _uiCamera;
    [SerializeField] SpawnerManager _spawnerManager = null;
    [SerializeField] PlayerDrag _playerDrag = null;
    [SerializeField] SplineDone _railSpline = null;
    [SerializeField] GameObject _player = null;
    [SerializeField] GameObject _station = null;
    [SerializeField] AudioSource _musicPlayer = null;
    [SerializeField] ScoreManager _scoreManager = null;
    [SerializeField] LocomotiveManager _locomotiveManager = null;


    public Camera UICamera => _uiCamera;
    public SpawnerManager SpawnerManager => _spawnerManager;
    public PlayerDrag PlayerDrag => _playerDrag;
    public SplineDone RailSpline => _railSpline;
    public GameObject Player => _player;
    public GameObject Station => _station;
    public AudioSource MusicPlayer => _musicPlayer;
    public ScoreManager ScoreManager => _scoreManager;
    public LocomotiveManager LocomotiveManager => _locomotiveManager;
}
