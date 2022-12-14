//From Seyed Morteza Kamali (https://gamedev.stackexchange.com/questions/126427/draw-circle-around-gameobject-to-indicate-radius) modified by ALBERT Esteban
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RangeIndicator : MonoBehaviour
{
    [Range(0, 50)][SerializeField] private int _segments = 50;
    private float _radius = 0;
    private LineRenderer _line;
    private TowerManager _towerManager = null;

    private void Awake()
    {
        _line = gameObject.GetComponent<LineRenderer>();
        _line.positionCount = _segments + 1;
        _line.useWorldSpace = false;
        _towerManager = GetComponentInParent<TowerManager>();
        
    }
    private void Start()
    {
        UpdateCircle();
    }

    public void UpdateCircle()
    {
        _radius = _towerManager.TowersData.Range;
        float x;
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


    public void EnableRangeIndicator(bool enabled)
    {
        _line.enabled = enabled;
    }

    public void ChangeIndicatorColor(Material material)
    {
        _line.material = material;
    }
}
