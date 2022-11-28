//From Seyed Morteza Kamali (https://gamedev.stackexchange.com/questions/126427/draw-circle-around-gameobject-to-indicate-radius) modified by ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RangeIndicator : MonoBehaviour
{
    [Range(0, 50)][SerializeField] private int _segments = 50;
    private float _radius = 0;
    private LineRenderer _line;

    private void Awake()
    {
        _line = gameObject.GetComponent<LineRenderer>();
        _line.positionCount = _segments + 1;
        _line.useWorldSpace = false;
        TowersDatas towerDatas = GetComponentInParent<TowerManager>().TowersData;
        _radius = towerDatas.Range;
        CreatePoints();
    }
    void Start()
    {

    }

    void CreatePoints()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (_segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * _radius;

            _line.SetPosition(i, new Vector3(x, 0.0f, z));

            angle += (360f / _segments);
        }
    }
}
