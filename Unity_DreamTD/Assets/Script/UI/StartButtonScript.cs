using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image _timerContainer;
    private Material _timerMaterial;
    [SerializeField]
    private StationBehaviour _trainStation;
    [SerializeField]
    private WavePreviewerManager _wavePreviewerManager;

    private Animator _animator;
    private SpawnerManager _spawnerManager;
    private WaveDatabase _waveDatabase;

    [Header("Information")]
    [SerializeField]
    private float _warningLerpValue;


    private float _lerpValue;
    private float _startTime;
    private float _delayValue;

    private bool _canStartWave = true;
    private bool _isFirstPhase = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spawnerManager = LevelReferences.Instance.SpawnerManager;
        _waveDatabase = WaveDatabaseManager.Instance.WaveDatabase;
        _timerMaterial = _timerContainer.material;

        _timerMaterial.SetFloat("_LerpValue", 0);
    }

    public void EndWave()
    {
        _animator.SetBool("WaveStarted", false);

        _startTime = Time.time;
        _delayValue = _waveDatabase.DelayBetweenWave;
        _canStartWave = true;
    }

    public void StartWave()
    {
        if (_canStartWave)
        {
            _canStartWave = false;
            _isFirstPhase = false;

            _lerpValue = 0f;

            _animator.SetBool("AlertMode", false);
            _animator.SetBool("WaveStarted", true);


            _spawnerManager.StartWaves();
            _trainStation.RestartTrain();
            _wavePreviewerManager.SetStartButton(this);
        }
    }


    private void Update()
    {
        if (_canStartWave & !_isFirstPhase)
        {
            _lerpValue = (Time.time - _startTime) / _delayValue;

            if (Time.time >= _startTime + _delayValue)
            {
                StartWave();
            }

            _timerMaterial.SetFloat("_LerpValue", _lerpValue);

            if (_lerpValue > _warningLerpValue & !_animator.GetBool("AlertMode"))
            {
                _animator.SetBool("AlertMode", true);
            }
        }
    }


}
