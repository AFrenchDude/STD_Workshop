//From Template
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEntity : MonoBehaviour
{
    [SerializeField]
    private PathFollower _pathFollower = null;

    public void SetPath(List<Vector3> path)
    {
        _pathFollower.SetPath(path);
    }
    private void Awake()
    {
        if (LevelReferences.HasInstance)
        {
            LevelReferences.Instance.SpawnerManager?.AddWaveEntityToList(this);
        }
    }
    private void OnDestroy()
    {
        if (LevelReferences.HasInstance)
        {
            LevelReferences.Instance.SpawnerManager?.RemoveWaveEntityToList(this);
        }
    }
}