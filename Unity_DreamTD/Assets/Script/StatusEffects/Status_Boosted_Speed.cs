using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Melinon Remy
public class Status_Boosted_Speed : MonoBehaviour
{
    [SerializeField] private float _slowDuration = 0.1f;
    private PathFollower _pathFollower = null;
    private float _slowStartTime = 0.0f;
    private float _originalSpeed = 0.0f;
    private float _addedSpeed = 5f;

    private void OnEnable()
    {
        _pathFollower = GetComponentInParent<PathFollower>();
        _originalSpeed = _pathFollower.OGSpeed;
        _slowStartTime = Time.time;
    }
    private void Update()
    {
        if (Time.time >= _slowStartTime + _slowDuration)
        {
            _pathFollower.SetSpeed(_originalSpeed);
            Destroy(this);
        }
    }

    public void ResetTimer()
    {
        _slowStartTime = Time.time;
    }

    public void AddSpeed(float addedSpeed)
    {
        _addedSpeed = addedSpeed;
        _pathFollower.SetSpeed(_originalSpeed + _addedSpeed);
    }
}
