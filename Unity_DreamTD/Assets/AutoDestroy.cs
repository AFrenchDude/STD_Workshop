using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeDestroy;

    private float _startTime;

    private void Awake()
    {
        _startTime = Time.time;
    }
    private void Update()
    {
        if( _startTime + _timeBeforeDestroy < _timeBeforeDestroy)
        {
            Destroy(gameObject);
        }
    }
}