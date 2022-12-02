//By ALBERT Esteban & ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private TowersDatas _towersData;

    [SerializeField]
    private ProjectileType _neutralProjectile;

    [SerializeField]
    private float _rotationSpeed = 1;

    [SerializeField]
    private List<Transform> _canonMuzzle = new List<Transform>();
    private int _muzzleIndx = 0;

    [SerializeField]
    private List<Transform> _canonPivot = new List<Transform>();

    private List<Damageable> _target = new List<Damageable>();
    private float _lastShotTime;
    public bool canShoot = false;

    //Allow to spawn One projectile by one for mortar.
    private AProjectile _lastProjectile = null;

    private AudioSource audioSource;

    public void setTowerData(TowersDatas towerData)
    {
        _towersData = towerData;
    }

    public void SetTarget(List<Damageable> target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {

        for (int i = 0; i < _canonMuzzle.Count; i++)
        {
            if (_target.Count >= 1)
            {

                if (_target[i] != null)
                {
                    Vector3 targetDirection = _target[i].TargetAnchor.transform.position - _canonPivot[i].transform.position;
                    _canonPivot[i].transform.rotation = Quaternion.Slerp(_canonPivot[i].transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * _rotationSpeed);
                }
            }
        }

    }
    private void OnEnable()
    {
        _lastShotTime = -_towersData.FireRate;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time >= _lastShotTime + _towersData.FireRate && canShoot && _lastProjectile == null)
        {
            Shoot();
            _lastShotTime = Time.time;
        }
    }

    private void Shoot()
    {

        audioSource.clip = _neutralProjectile.shotSound[Random.Range(0, _neutralProjectile.shotSound.Count)];
        audioSource.Play();
        AProjectile spawnedProjectile;
        
        if(_towersData.Projectiles.Count > 0)
        {
            if (_towersData.hasProjectiles(_muzzleIndx))
            {
                ProjectileType currentProjectile = _towersData.Projectiles[_muzzleIndx].ProjectileType;
                spawnedProjectile = Instantiate(currentProjectile.projectile.GetComponent<AProjectile>());
                _towersData.ReduceProjAmmount(_muzzleIndx, 1);

            }

            else
            {
                spawnedProjectile = Instantiate(_neutralProjectile.projectile.GetComponent<AProjectile>());
            }
        }
        
        else
        {
            spawnedProjectile = Instantiate(_neutralProjectile.projectile.GetComponent<AProjectile>());
        }

        spawnedProjectile.transform.position = _canonMuzzle[_muzzleIndx].transform.position;
        spawnedProjectile.transform.rotation = _canonMuzzle[_muzzleIndx].transform.rotation;

        spawnedProjectile.GetComponent<AProjectile>().SetTarget(_target[_muzzleIndx].TargetAnchor);
        spawnedProjectile.GetComponent<AProjectile>().SetSpeed(_towersData.ProjectileSpeed);
        spawnedProjectile.GetComponent<AProjectile>().SetFireType(_towersData.FireType);

        spawnedProjectile.GetComponent<Damager>().SetDamage(_towersData.Damage);

        //Set up muzzle index (For Double canon)

        if (_muzzleIndx >= _canonMuzzle.Count - 1)
        {
            _muzzleIndx = 0;
        }
        else
        {
            _muzzleIndx++;
        }

        //Set up mortar curve (For Mortar)
        BellShapedCurve curve = spawnedProjectile.GetComponent<BellShapedCurve>();

        if (_towersData.FireType == TowersDatas.fireType.Mortar)
        {
            curve.enabled = true;
            curve.SetUpCurve(_target[_muzzleIndx].transform);

            _lastProjectile = spawnedProjectile;

            // Adapt projectile speed by enemy distance
<<<<<<< HEAD
            float speed = _towersData.ProjectileSpeed * (_target[_muzzleIndx].transform.position - transform.position).magnitude / _towersData.Range;
            spawnedProjectile.GetComponent<AProjectile>().SetSpeed(speed);
=======
            float speed = _towersData.ProjectileSpeed * (_target[_muzzleIndx].transform.position - transform.position).sqrMagnitude / (_towersData.Range * _towersData.Range);
            //spawnedProjectile.GetComponent<AProjectile>().SetSpeed(speed);
>>>>>>> 9bf052a7dc8f9b44331cd42c98def54c48239595
        }
        else
        {
            curve.enabled = false;
        }


    }

    public ProjectileType getCurrentProjectileType
    {
        get { return _towersData.Projectiles[_muzzleIndx].ProjectileType; }
    }

}
