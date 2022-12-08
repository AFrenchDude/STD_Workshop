using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavePreviewerManager : MonoBehaviour
{
    private WaveDatabase _waveDatabase;
    private RectTransform _rectTransform;

    [SerializeField]
    WavePreviewElement _previewElementPrefab;

    [SerializeField]
    private AnimationCurve _leftMovementCurve;

    [SerializeField]
    private float _movementDistance;

    [SerializeField]
    private float _speed = 1f;

    //Ui text
    [SerializeField]
    private TextMeshProUGUI _waveIndexIndicator;

    private void Awake()
    {
        _waveDatabase = WaveDatabaseManager.Instance.WaveDatabase;
        _rectTransform = GetComponent<RectTransform>();

        foreach (WaveSet waveSet in _waveDatabase.Waves)
        {
            WavePreviewElement wavePreviewElement = Instantiate(_previewElementPrefab, transform);

            wavePreviewElement.SetWave(waveSet);
        }

        SetUpCurrentWave();
    }

    public void StartNextWave()
    {

        SetUpCurrentWave();
        StartCoroutine(MovementCoroutine(_rectTransform.anchoredPosition.x , 0));
        
    }

    public void SetUpCurrentWave()
    {
        string currentWave = (1 + LevelReferences.Instance.SpawnerManager.getCurrentWave).ToString();
        string maxWave = _waveDatabase.Waves.Count.ToString();
        _waveIndexIndicator.text = currentWave + " / " + maxWave;
    }

    public IEnumerator MovementCoroutine(float startPosisiton,  float lerp)
    {
        yield return new WaitForFixedUpdate();
        lerp += Time.fixedDeltaTime * _speed;

        float newValue = startPosisiton + (_leftMovementCurve.Evaluate(lerp) * _movementDistance);
        _rectTransform.anchoredPosition = new Vector2(newValue, _rectTransform.anchoredPosition.y);

        if(lerp >= 1)
        {
            _rectTransform.anchoredPosition = new Vector2(startPosisiton + _movementDistance, _rectTransform.anchoredPosition.y);
        }
        else
        {
            StartCoroutine(MovementCoroutine(startPosisiton, lerp));
        }

    }


}
