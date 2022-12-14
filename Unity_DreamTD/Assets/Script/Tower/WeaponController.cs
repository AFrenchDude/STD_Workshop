//By ALBERT Esteban & ALEXANDRE Dorian
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

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
    private List<VisualEffect> _fireBoom = new List<VisualEffect>();

    [SerializeField]
    private List<Transform> _canonPivot = new List<Transform>();

    private List<Damageable> _target = new List<Damageable>();
    private float _lastShotTime;
    public bool canShoot = false;

    //Allow to spawn One projectile by one for mortar.
    private AProjectile _lastProjectile = null;

    [SerializeField] private List<AudioSource> audioSources;

    public void setTowerData(TowersDatas towerData)
    {
        _towersData = towerData;
    }

    public void SetTarget(List<Damageable> target)
    {
        _target = target;
    }

    public void ResetMuzzleAndPivotList()
    {
        _canonMuzzle.Clear();
        _canonPivot.Clear();
        _fireBoom.Clear();
    }

    public void AddMuzzle(Transform transform)
    {
        _canonMuzzle.Add(transform);
    }

    public void AddPivot(Transform pivot)
    {
        _canonPivot.Add(pivot);
    }

    public void AddFireEffect(VisualEffect vfx)
    {
        _fireBoom.Add(vfx);
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


                    Quaternion Rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Quaternion.LookRotation(targetDirection).eulerAngles.y, transform.rotation.eulerAngles.z);
                    _canonPivot[i].transform.rotation = Quaternion.Slerp(_canonPivot[i].transform.rotation, Rotation, Time.deltaTime * _rotationSpeed);


                }
            }
        }

    }
    private void OnEnable()
    {
        _lastShotTime = -_towersData.FireRate;
    }

    private void Update()
    {
        if (Time.time >= _lastShotTime + _towersData.FireRate && canShoot)
        {
            Shoot();
            _lastShotTime = Time.time;
        }
    }

    private void Shoot()
    {
        if (audioSources[_muzzleIndx] != null)
        {
            audioSources[_muzzleIndx].clip = _neutralProjectile.shotSound[Random.Range(0, _neutralProjectile.shotSound.Count)];
            audioSources[_muzzleIndx].Play();
        }
        AProjectile spawnedProjectile = null;

        if (_towersData.ProjectilesList.Count > 0)
        {
            if (_towersData.hasProjectiles(_muzzleIndx))
            {
                ProjectileType currentProjectile = _towersData.ProjectilesList[_muzzleIndx].ProjectileType;
                spawnedProjectile = Instantiate(currentProjectile.projectile.GetComponent<AProjectile>());



                _towersData.ReduceProjAmmount(_muzzleIndx, 1);
            }
            else
            {
                if (_neutralProjectile.projectile.GetComponent<AProjectile>() != null)
                {
                    spawnedProjectile = Instantiate(_neutralProjectile.projectile.GetComponent<AProjectile>());
                }
                else
                {
                    return;
                }
            }


        }
        else
        {
            spawnedProjectile = Instantiate(_neutralProjectile.projectile.GetComponent<AProjectile>());
        }

        spawnedProjectile.transform.position = _canonMuzzle[_muzzleIndx].transform.position;
        spawnedProjectile.transform.rotation = _canonMuzzle[_muzzleIndx].transform.rotation;

        spawnedProjectile.GetComponent<AProjectile>().SetFireType(_towersData.FireType);
        spawnedProjectile.GetComponent<AProjectile>().SetTarget(_target[_muzzleIndx].TargetAnchor);
        spawnedProjectile.GetComponent<AProjectile>().SetSpeed(_towersData.ProjectileSpeed);

        Damager projectileDamager = spawnedProjectile.GetComponent<Damager>();

        projectileDamager.SetDamage(_towersData.Damage);


        //Test for miraculous bullet
        if (_towersData.hasProjectiles(_muzzleIndx))
        {
            projectileDamager.ActiveMiraculousBullet();
        }

        //Fire boom effect

        if (_fireBoom[_muzzleIndx].gameObject.active == false)
        {
            _fireBoom[_muzzleIndx].gameObject.SetActive(true);
        }
        _fireBoom[_muzzleIndx].Play();

        //Set up muzzle index (For Double canon)

        if (_muzzleIndx >= _canonMuzzle.Count - 1)
        {
            _muzzleIndx = 0;
        }
        else
        {
            _muzzleIndx++;
        }

        //Set up Mortar AOE
        if (_towersData.FireType == TowersDatas.fireType.Mortar)
        {
            spawnedProjectile.GetComponent<Damager>().SetMortarRadius(_towersData.AOERadius);
        }


        spawnedProjectile.GetComponent<Damager>().SetFireType(_towersData.FireType);





    }

    public ProjectileType getCurrentProjectileType
    {
        get { return _towersData.ProjectilesList[_muzzleIndx].ProjectileType; }
    }

}
