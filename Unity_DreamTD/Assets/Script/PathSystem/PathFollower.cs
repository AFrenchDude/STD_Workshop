//By ALBERT Esteban
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private float _rotationSpeed = 1.0f;
    [SerializeField] private float _waypointThreshold = 1.0f;
    [SerializeField] private bool _loop = false;

    private float _originalMovementSpeed = 1.0f;
    private List<Vector3> _pathWaypoints;
    private int _waypointIndex = 0;
    private int _lastWaypointIndexReached = 0;


    public float Speed => _movementSpeed;
    public float OGSpeed => _originalMovementSpeed;
    public bool Loop => _loop;

    public UnityEvent<PathFollower> LastWaypointReached;
    public void SetOriginalSpeed(float speed)
    {
        _originalMovementSpeed = speed;
    }
    public void SetSpeed(float speed)
    {
        _movementSpeed = speed;
        _rotationSpeed = speed;
    }

    public void SetPath(List<Vector3> path)
    {
        _pathWaypoints = path;

        Vector3 spawnLocation = _pathWaypoints[0]; //If index exception, let the wave start after the path finished inizialising
        if (spawnLocation != null)
        {
            transform.position = spawnLocation;
        }
    }

    public float getPathDistance
    {
        get{return _waypointIndex * 10000 + (_pathWaypoints[_waypointIndex] - transform.position).magnitude;}
    }

    private void Update()
    {
        if (_pathWaypoints == null)
        {
            return;
        }

        RotateToGoalDirection();
        transform.position += transform.forward * _movementSpeed * Time.deltaTime;
        CheckTargetReached();
    }

    private void CheckTargetReached()
    {
        if (GetGoalDirection().sqrMagnitude <= (_waypointThreshold * _waypointThreshold))
        {
            if (_waypointIndex + 1 < _pathWaypoints.Count)
            {
                _lastWaypointIndexReached = _waypointIndex;
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
        Vector3 goalDirection = _pathWaypoints[_waypointIndex] - transform.position;
        return goalDirection;
    }
    private void RotateToGoalDirection()
    {
        Quaternion goalRotation = Quaternion.LookRotation(GetGoalDirection());
        transform.rotation = Quaternion.Lerp(transform.rotation, goalRotation, _rotationSpeed * Time.deltaTime);
    }
    private void LastWayPointReached()
    {
        LastWaypointReached.Invoke(this);
        if (_loop)
        {
            _waypointIndex = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetPathProgress()
    {
        float generalProgress = _lastWaypointIndexReached / (_pathWaypoints.Count - 1);

        float targetToThisDistance = (_pathWaypoints[_waypointIndex] - transform.position).sqrMagnitude;
        float lastWaypointToTargetDistance = (_pathWaypoints[_waypointIndex] - _pathWaypoints[_lastWaypointIndexReached]).sqrMagnitude;
        float currentPathProgress = targetToThisDistance / lastWaypointToTargetDistance;

        return generalProgress * 2.0f + currentPathProgress;
    }

    public void SetNewSpeed(float newSpeed)
    {
        _movementSpeed = newSpeed;
    }
}
