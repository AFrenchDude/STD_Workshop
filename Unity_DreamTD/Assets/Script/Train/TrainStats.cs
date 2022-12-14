//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Train Stats", fileName = "Train_Stats")]
public class TrainStats : ScriptableObject
{
    [SerializeField] private int _maxWagonCount = 3;
    [SerializeField] private float _wagonDistanceMargin = 5;
    [SerializeField] private List<float> _speed = new List<float>();
    [SerializeField] private float _waitTimeBetweenTransfers = 0.2f;
    [SerializeField] private List<int> _wagonMaxStorage = new List<int>();
    [SerializeField] private int _scoreOnTransfer = 1;
    [SerializeField] private int _scoreOnUpgrade = 1000;
    [SerializeField] private List<GameObject> _meshList;

    public int MaxWagonCount => _maxWagonCount;
    public float WagonMargin => _wagonDistanceMargin;
    public List<float> SpeedLevels => _speed;
    public List<int> WagonMaxStorageLevels => _wagonMaxStorage;
    public float WaitTime => _waitTimeBetweenTransfers;
    public int ScoreOnTransfer => _scoreOnTransfer;
    public int ScoreOnUpgrade => _scoreOnUpgrade;
    public List<GameObject> MeshList => _meshList;
}
