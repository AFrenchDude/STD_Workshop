//By ALBERT Estbeban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] float _fireCooldown = 1.0f;
    [SerializeField] AProjectile _projectilePrefab = null;
    [SerializeField] Transform _canonMuzzle = null;
    [SerializeField] Transform _canonPivot = null;

    private Damageable _target = null;
    private float _lastShotTime;

    public void SetTarget(Damageable target)
    {
        _target = target;
    }

    private void Update()
    {
        Vector3 targetDirection = _target.TargetAnchor.transform.position - _canonPivot.transform.position;
        _canonPivot.transform.rotation = Quaternion.LookRotation(targetDirection);
        if (Time.time >= _lastShotTime + _fireCooldown)
        {
            Shoot();
            _lastShotTime = Time.time;
        }
    }

    private void Shoot()
    {
        AProjectile spawnedProjectile = Instantiate(_projectilePrefab);
        spawnedProjectile.transform.position = _canonMuzzle.transform.position;
        spawnedProjectile.transform.rotation = _canonMuzzle.transform.rotation;
    }
}
