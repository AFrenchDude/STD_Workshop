//From Template
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEntity : MonoBehaviour
{
    [SerializeField]
    private PathFollower _pathFollower = null;

    public void SetPath(Path path)
    {
        _pathFollower.SetPath(path);
    }
}