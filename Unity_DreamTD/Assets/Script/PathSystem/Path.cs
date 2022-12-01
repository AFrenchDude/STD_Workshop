//From Template, modified by ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints = null;
    private List<List<Vector3>> _laneList;
    [Range(1, 5)] [SerializeField] private int _laneNumber = 1;
    [SerializeField] private float _laneOffset = 1;

    [SerializeField] private bool _showGizmos = true;
    [SerializeField] private Color _lineColor = Color.white;
    private readonly Vector3 _offset = new Vector3(0, 0.5f, 0);

    private Vector3 _startDirection;
    public Vector3 getStartDirection
    {
        get { return _startDirection.normalized; }
    }

    public List<List<Vector3>> LanesList => _laneList;

    private void Awake()
    {
        _laneList = new List<List<Vector3>>();
        int offsetIndex = 0;
        for (int i = 0; i < _laneNumber; i++)
        {
            float newLaneOffset = offsetIndex * _laneOffset;
            if (i % 2 == 0)
            {
                newLaneOffset = (-newLaneOffset);
                offsetIndex++;
            }
            _laneList.Add(PopulateNewLaneList(newLaneOffset));
        }

        //Set start directon of path
        _startDirection = _waypoints[1].position - _waypoints[0].position;
    }

    private List<Vector3> PopulateNewLaneList(float laneOffset)
    {
        List<Vector3> populatedList = new List<Vector3>();
        Vector3 newPosition;

        newPosition = (_waypoints[0 + 1].position - _waypoints[0].position).normalized;
        newPosition = Vector3.Cross(Vector3.up, newPosition) * laneOffset;
        newPosition += _waypoints[0].position;
        populatedList.Add(newPosition);

        for (int i = 1; i < _waypoints.Count - 1; i++)
        {
            newPosition = (_waypoints[i + 1].position - _waypoints[i - 1].position).normalized;
            newPosition = Vector3.Cross(Vector3.up, newPosition) * laneOffset;
            newPosition += _waypoints[i].position;
            populatedList.Add(newPosition);
        }

        newPosition = (_waypoints[_waypoints.Count - 2].position - _waypoints[_waypoints.Count - 1].position).normalized;
        newPosition = Vector3.Cross(Vector3.up, newPosition) * (-laneOffset);
        newPosition += _waypoints[_waypoints.Count - 1].position;
        populatedList.Add(newPosition);

        return populatedList;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_showGizmos == false || _waypoints == null)
        {
            return;
        }

        for (int i = 0, length = _waypoints.Count - 1; i < length; i++)
        {
            Transform currentWaypoint = _waypoints[i];
            Transform nextWaypoint = _waypoints[i + 1];
            if (currentWaypoint != null && nextWaypoint != null)
            {
                Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
                var color = Handles.color;
                Handles.color = _lineColor;
                {
                    Handles.DrawLine(currentWaypoint.position + _offset, nextWaypoint.position + _offset);
                }
                Handles.color = color;
            }
        }
    }
#endif //UNITY_EDITOR

}

