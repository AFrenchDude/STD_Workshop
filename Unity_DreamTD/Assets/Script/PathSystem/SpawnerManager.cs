//From Template, modified by ALBERT Esteban
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
    private int _currentWaveRunning = 0;

    [System.NonSerialized]
    private Coroutine _waitForNextWaveCoroutine;

    private int _waveEntityListCount = 0;
    private bool _isWaitingForLastEntityDeath = false;

    [SerializeField]
    public UnityEvent<SpawnerManager, SpawnerStatus, int> WaveStatusChanged_UnityEvent = null;

    public delegate void SpawnerEvent(SpawnerManager sender, SpawnerStatus status, int runningWaveCount);
    public event SpawnerEvent WaveStatusChanged = null;

    private void Update()
    {
        if (_isWaitingForLastEntityDeath)
        {
            if (_waveEntityListCount <= 0)
            {
                EndGameCondition.Instance.PlayerVictory(); // No enemy left: end game
                _isWaitingForLastEntityDeath = false;
            }
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
            StartNewWaveSet();
        }
    }

    public void StartNewWaveSet()
    {
        LevelReferences.Instance.MusicPlayer.Play();
        _currentWaveSetIndex += 1;
        var waveDatabase = WaveDatabaseManager.Instance.WaveDatabase;

        if (waveDatabase.Waves.Count > _currentWaveSetIndex)
        {
            WaveSet waveSet = waveDatabase.Waves[_currentWaveSetIndex];
            List<Wave> waves = waveSet.Waves;

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
                var spawner = _spawners[i];
                spawner.StartWave(waves[i]);
                spawner.WaveEnded.RemoveListener(Spawner_OnWaveEnded);
                spawner.WaveEnded.AddListener(Spawner_OnWaveEnded);

                WaveStatusChanged?.Invoke(this, SpawnerStatus.WaveRunning, _currentWaveRunning);
                WaveStatusChanged_UnityEvent?.Invoke(this, SpawnerStatus.WaveRunning, _currentWaveRunning);
            }
        }
        else
        {
            _isWaitingForLastEntityDeath = true; //No Waves left => Set flag to wait for game's end
        }
    }

    private void Spawner_OnWaveEnded(EntitySpawner entitySpawner, Wave wave)
    {
        LevelReferences.Instance.MusicPlayer.Stop();
        entitySpawner.WaveEnded.RemoveListener(Spawner_OnWaveEnded);

        _currentWaveRunning -= 1;

        WaveStatusChanged?.Invoke(this, SpawnerStatus.Inactive, _currentWaveRunning);
        WaveStatusChanged_UnityEvent?.Invoke(this, SpawnerStatus.Inactive, _currentWaveRunning);

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