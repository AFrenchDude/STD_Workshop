//By ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField]
    private TowersDatas _towersDatas;

    [Header("References")]

    private Sentry _sentry;
    private WeaponController _weaponController;

    [SerializeField]
    private SphereCollider _rangeDetector;

    private void OnEnable()
    {
        _sentry = GetComponent<Sentry>();
        _weaponController = GetComponent<WeaponController>();

        //Apply statistics to every scripts
        ApplyStats(_towersDatas);
    }

    public void ApplyStats(TowersDatas towerData)
    {
        _towersDatas = Instantiate(towerData); // Create a new instance for scriptable object

        _weaponController.setTowerData(_towersDatas);
        _rangeDetector.radius = _towersDatas.Range;
    }
}
