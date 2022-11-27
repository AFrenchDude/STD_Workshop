//By ALBERT Estbeban & ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private TowersDatas _towersData;

    [SerializeField]
    private Type _neutralProjectile;

    [SerializeField]
    private List<Transform> _canonMuzzle = new List<Transform>();
    private int _muzzleIndx = 0;

    [SerializeField] Transform _canonPivot = null;

    private Damageable _target = null;
    private float _lastShotTime;

    public void setTowerData(TowersDatas towerData)
    {
        _towersData = towerData;
    }

    public void SetTarget(Damageable target)
    {
        _target = target;
    }

    private void Update()
    {
        Vector3 targetDirection = _target.TargetAnchor.transform.position - _canonPivot.transform.position;
        _canonPivot.transform.rotation = Quaternion.LookRotation(targetDirection);
        if (Time.time >= _lastShotTime + _towersData.FireRate)
        {
            Shoot();
            _lastShotTime = Time.time;
        }
    }
    
    private void Shoot()
    {
        AProjectile spawnedProjectile;

        Type currentProjectile = _towersData.Projectiles[_muzzleIndx].ProjectileType;

        if (_towersData.hasProjectiles(_muzzleIndx))
        {
            spawnedProjectile = Instantiate(currentProjectile.projectile.GetComponent<AProjectile>());
            _towersData.ReduceProjAmmount(_muzzleIndx, 1);
            
        }
        else
        {
            spawnedProjectile = Instantiate(_neutralProjectile.projectile.GetComponent<AProjectile>());
        }

        spawnedProjectile.transform.position = _canonMuzzle[_muzzleIndx].transform.position;
        spawnedProjectile.transform.rotation = _canonMuzzle[_muzzleIndx].transform.rotation;

        spawnedProjectile.GetComponent<Damager>().SetDamage(_towersData.Damage);

        //Set up muzzle index (For Double canon)

        if(_muzzleIndx >= _canonMuzzle.Count - 1)
        {
            _muzzleIndx = 0;
        }
        else
        {
            _muzzleIndx++;
        }

    }
    
}
