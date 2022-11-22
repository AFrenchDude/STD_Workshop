//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : MonoBehaviour, ICellChild
{

    [SerializeField] TargetPriority _targetPriority = TargetPriority.Nearest;
    [SerializeField] DamageableDetector _damageableDetector = null;
    [SerializeField] WeaponController _weaponController = null;

    private void Awake()
    {
        //enabled = false;
        _weaponController.enabled = false;
    }
    public void Enable(bool isEnabled)
    {
        enabled = isEnabled;
    }

    private void Update()
    {
        if (_damageableDetector.HasAnyDamageableInRange() == true)
        {
            if (_weaponController.enabled == false)
            {
                _weaponController.enabled = true;
            }
            Damageable targetedDamageable = _damageableDetector.GetDamageable(_targetPriority);
            _weaponController.SetTarget(targetedDamageable);
        }
        else
        {
            if (_weaponController.enabled == true)
            {
                _weaponController.enabled = false;
            }
        }
    }

    // Interfaces
    public Transform GetTransform()
    {
        return transform;
    }

    public void OnSetChild()
    {
        Enable(true);
    }
}
