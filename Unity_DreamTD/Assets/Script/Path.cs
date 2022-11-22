//From Template
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints = null;
    [SerializeField] private bool _showGizmos = true;
    [SerializeField] private Color _lineColor = Color.white;

    [SerializeField] private PathFollower _pathFollower = null; //For testing purpose until spawning is properly implemented

    private readonly Vector3 _offset = new Vector3(0, 0.5f, 0);

    public List<Transform> Waypoints => _waypoints;


    private void Awake()
    {
        _pathFollower.SetPath(_waypoints); //For testing purpose until spawning is properly implemented
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

