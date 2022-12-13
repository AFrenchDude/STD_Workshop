//From Template, modified by ALBERT Esteban and ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SpawnerIndex
{
    Spawner00,
    Spawner01,
    Spawner02,
}

public enum SpawnerStatus
{
    Inactive = 0,
    WaveRunning
}

public class SpawnerManager : MonoBehaviour
{
    [SerializeField]
    private List<EntitySpawner> _spawners = null;

    [SerializeField]
    private bool _autoStartNextWaves = false;

    [System.NonSerialized]
    private int _currentWaveSetIndex = -1;

    [System.NonSerialized]
    [SerializeField]
    private int _currentWaveRunning = 0;

    public int getCurrentWave
    {
        get { return _currentWaveSetIndex; }
    }

    [System.NonSerialized]
    private Coroutine _waitForNextWaveCoroutine;

    //[System.NonSerialized]
    [SerializeField]
    private SpawnerStatus _spawnerState;

    [SerializeField]
    private int _waveEntityListCount = 0;

    [SerializeField]
    private bool _isLastWave = false;

    [SerializeField]
    public UnityEvent<SpawnerManager, SpawnerStatus, int> WaveStatusChanged_UnityEvent = null;

    public delegate void SpawnerEvent(SpawnerManager sender, SpawnerStatus status, int runningWaveCount);
    public event SpawnerEvent WaveStatusChanged = null;

    private bool _waveEnded;

    private void Awake()
    {
        _spawnerState = SpawnerStatus.Inactive;
    }

    private void Start()
    {
        SetUpOriginPreviewForAllSpawner();
    }

    public bool isWaveRunning
    {
        get { return _spawnerState == SpawnerStatus.WaveRunning; }
    }
    private void Update()
    {
        if (_isLastWave)
        {
            if (_waveEntityListCount <= 0 && NoEntityLeftToSpawn())
            {
                EndGameCondition.Instance.PlayerVictory(); // No enemy left: end game
                _isLastWave = false;
                _spawnerState = SpawnerStatus.Inactive;
            }
        }

        else if (_waveEntityListCount <= 0 & _waveEnded)
        {
            _spawnerState = SpawnerStatus.Inactive;
        }

    }

    public void AddWaveEntityToList(WaveEntity waveEntity)
    {
        if (waveEntity != null)
        {
            _waveEntityListCount++;
        }
    }
    public void RemoveWaveEntityToList(WaveEntity waveEntity)
    {
        if (waveEntity != null)
        {
            _waveEntityListCount--;
        }
    }

    [ContextMenu("Start waves")]
    public void StartWaves()
    {
        // Start a new wave set only if there are no currently a wave running
        if (_currentWaveRunning <= 0)
        {
            _waveEnded = false;
            _spawnerState = SpawnerStatus.WaveRunning;
            StartNewWaveSet();            
        }
            
    }

    public void StartNewWaveSet()
    {

        if (LevelReferences.Instance.MusicPlayer != null)
        {
            LevelReferences.Instance.MusicPlayer.Play();

        }
        _currentWaveSetIndex += 1;
        var waveDatabase = WaveDatabaseManager.Instance.WaveDatabase;

        LevelReferences.Instance.DebugDataSaver.CreateNewWave(_currentWaveSetIndex);

        if (waveDatabase.Waves.Count > _currentWaveSetIndex)
        {
            WaveSet waveSet = waveDatabase.Waves[_currentWaveSetIndex];
            _currentWaveSet = waveSet;
            List<Wave> waves = waveSet.Waves;

            EntitySpawner spawner = null;

            for (int i = 0, length = _spawners.Count; i < length; i++)
            {
                if (i >= waves.Count)
                {
                    Debug.LogWarningFormat("{0}.StartNewWaveSet() There are more spawner ({1}) than wave ({2}), discarding wave.", GetType().Name, _spawners.Count, waves.Count);
                    break;
                }
                if (waves[i] == null)
                {
                    Debug.LogWarningFormat("{0}.StartNewWaveSet() Null reference found in WaveSet at index {1}, ignoring.", GetType().Name, i);
                    break;
                }
                _currentWaveRunning += 1;
                spawner = _spawners[i];
                spawner.StartWave(waves[i]);

                spawner.WaveEnded.RemoveListener(Spawner_OnWaveEnded);
                spawner.WaveEnded.AddListener(Spawner_OnWaveEnded);
            }



            if (_currentWaveSetIndex >= waveDatabase.Waves.Count - 1) // index is last index == index is last wave
            {
                _isLastWave = true; //At last wave => Set flag to instantly win, no need to start next wave
            }

        }
        else
        {
            _isLastWave = true; //No Waves left => Set flag to wait for game's end, used as failsafe
        }
    }

    public bool NoEntityLeftToSpawn()
    {
        bool remains = true;
        foreach (EntitySpawner spawner in _spawners)
        {
            Debug.Log(spawner.name + spawner.hasWaveElementLeft.ToString());
            if (spawner.hasWaveElementLeft)
            {
                remains = false;
            }
        }

        return remains;
    }



    private WaveSet _currentWaveSet;
    private void Spawner_OnWaveEnded(EntitySpawner entitySpawner, Wave wave)
    {
        Debug.LogWarning("Spawning new wave set");

        //LevelReferences.Instance.MusicPlayer.Stop();
        entitySpawner.WaveEnded.RemoveListener(Spawner_OnWaveEnded);

        _currentWaveRunning -= 1;

        if (NoEntityLeftToSpawn() && _isLastWave == false)
        {
            Debug.Log("Start next wave preview information send");
            WaveStatusChanged?.Invoke(this, SpawnerStatus.Inactive, _currentWaveRunning);
            WaveStatusChanged_UnityEvent?.Invoke(this, SpawnerStatus.Inactive, _currentWaveRunning);

            _waveEnded = true;

            SetUpOriginPreviewForAllSpawner();


            // should we run a new wave?
            if (_autoStartNextWaves == true && _currentWaveRunning <= 0)
            {
                // prevent overlapping routines
                if (_waitForNextWaveCoroutine != null)
                {
                    StopCoroutine(_waitForNextWaveCoroutine);
                }
                _waitForNextWaveCoroutine = StartCoroutine(WaitForNewWaveSet());
            }
        }
    }

    private void SetUpOriginPreviewForAllSpawner()
    {
        //Set wave origin to next wave

        var waveDatabase = WaveDatabaseManager.Instance.WaveDatabase;
        EntitySpawner spawner = null;

        if (waveDatabase.Waves.Count > _currentWaveSetIndex + 1)
        {
            WaveSet waveSet = waveDatabase.Waves[_currentWaveSetIndex + 1];
            List<Wave> waves = waveSet.Waves;

            for (int i = 0, length = _spawners.Count; i < length; i++)
            {

                spawner = _spawners[i];
                spawner.SetOriginActivationForNextWave(waves[i]);

            }
        }
    }

    private IEnumerator WaitForNewWaveSet()
    {
        var waveDatabase = WaveDatabaseManager.Instance.WaveDatabase;
        float waitingDuration = waveDatabase.Waves[_currentWaveSetIndex].WaitingDurationBefore + waveDatabase.DelayBetweenWave;

        if (_currentWaveSetIndex - 1 > 0)
        {
            waitingDuration += waveDatabase.Waves[_currentWaveSetIndex - 1].WaitingDurationAfter + waveDatabase.DelayBetweenWave;
        }

        Debug.LogFormat("Waiting {0} seconds until next wave.", waitingDuration);
        yield return new WaitForSeconds(waitingDuration);

        _waitForNextWaveCoroutine = null;


        StartNewWaveSet();
    }
 
}