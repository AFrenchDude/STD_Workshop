//From Template, modified by ALBERT Esteban
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _instancesRoot = null;

    [SerializeField]
    private Path _path = null;

    private int _pathLaneIndex = 0;

    [System.NonSerialized]
    private Timer _timer = new Timer();

    [System.NonSerialized]
    private Wave _wave = null;
    public Wave SpawnerWave => _wave;

    [System.NonSerialized]
    private List<WaveEntity> _runtimeWaveEntities = new List<WaveEntity>();

    public UnityEvent<EntitySpawner, Wave> WaveStarted = null;
    public UnityEvent<EntitySpawner, Wave> WaveEnded = null;
    public UnityEvent<EntitySpawner, WaveEntity> EntitySpawned = null;

    //public event System.Action<EntitySpawner, WaveEntity> EntityDestroyed = null;

    public void StartWave(Wave wave)
    {
        _wave = new Wave(wave);
        _timer.Set(wave.DurationBetweenSpawnedEntity).Start();
        WaveStarted?.Invoke(this, wave);
        InstantiateNextWaveElement();
    }

    private WaveEntity InstantiateEntity(WaveEntity entityPrefab)
    {
        WaveEntity entityInstance = Instantiate(entityPrefab, _instancesRoot);
        _runtimeWaveEntities.Add(entityInstance);
        EntitySpawned?.Invoke(this, entityInstance);
        return entityInstance;
    }

    public bool hasWaveElementLeft
    {
        get { return _wave.HasWaveElementsLeft; }
    }

    private void InstantiateNextWaveElement()
    {
        if (_wave.HasWaveElementsLeft == true)
        {
            var nextEntity = _wave.GetNextWaveElement();

            if (WaveDatabaseManager.Instance.WaveDatabase.GetWaveElementFromType(nextEntity.NightmareData, out WaveEntity outEntity) == true)
            {
                outEntity = InstantiateEntity(outEntity);
                outEntity.GetComponent<NightmareManager>().SetEnemyData(nextEntity.NightmareData);

                //Rotate to path direction at spawning
                outEntity.transform.rotation = Quaternion.LookRotation(_path.getStartDirection);

                outEntity.SetPath(_path.LanesList[nextEntity.SpawningLane-1]);
                
                

                _timer.Set(_wave.DurationBetweenSpawnedEntity + nextEntity.ExtraDurationAfterSpawned).Start();
            }
            else
            {
                Debug.LogErrorFormat("{0}.UpdateWave() cannot GetWaveElementFromType {1}, no corresponding type found in database.", GetType().Name, nextEntity.NightmareData.nighmareType);
                return;
            }
        }
        else
        {
            WaveEnded?.Invoke(this, _wave);
        }
    }

    private void Update()
    {
        UpdateWave();
    }

    private void UpdateWave()
    {
        if (_timer != null)
        {
            bool shouldInstantiateEntity = _timer.Update();

            if (shouldInstantiateEntity == true)
            {
                InstantiateNextWaveElement();
            }
        }
    }
}