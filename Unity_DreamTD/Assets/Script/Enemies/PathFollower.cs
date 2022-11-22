//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private float _rotationSpeed = 1.0f;
    [SerializeField] private float _waypointThreshold = 1.0f;
    [SerializeField] private bool _loop = false;

    private List<Transform> _pathWaypoints;
    private int _waypointIndex = 0;

    public void SetPath(List<Transform> path)
    {
        _pathWaypoints = path;

        Transform spawnLocation = _pathWaypoints[0];
        if (spawnLocation != null)
        {
            transform.position = spawnLocation.position;
        }
    }

    private void Update()
    {
        if (_pathWaypoints == null)
        {
            return;
        }
        RotateToGoalDirection();

        Vector3 normalizedDirection = GetGoalDirection().normalized;
        transform.position += normalizedDirection * _movementSpeed * Time.deltaTime;

        if (GetGoalDirection().magnitude <= _waypointThreshold)
        {
            if (_waypointIndex + 1 < _pathWaypoints.Count)
            {
                _waypointIndex++;
            }
            else
            {
                LastWayPointReached();
            }
        }
    }


    private Vector3 GetGoalDirection()
    {
        Vector3 goalDirection = _pathWaypoints[_waypointIndex].position - transform.position;
        return goalDirection;
    }
    private void RotateToGoalDirection()
    {
        Quaternion goalRotation = Quaternion.LookRotation(GetGoalDirection());
        transform.rotation = Quaternion.Lerp(transform.rotation, goalRotation, _rotationSpeed * Time.deltaTime);
    }
    private void LastWayPointReached()
    {
        if (_loop)
        {
            _waypointIndex = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
