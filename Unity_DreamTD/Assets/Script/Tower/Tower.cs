//By ALBERT Esteban
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IPickerGhost
{
    public TargetPriority _targetPriority = TargetPriority.Nearest;
    private DamageableDetector _damageableDetector = null;
    private WeaponController _weaponController = null;
    private RangeIndicator _rangeIndicator = null;

    private TowersDatas _datas;
    private List<Damageable> allTargetedDamageable = new List<Damageable>();

    private int _price = 0;
    public int Price => _price;
    public RangeIndicator RangeIndicator => _rangeIndicator;

    private void Awake()
    {
        Enable(false);
        _rangeIndicator = gameObject.GetComponentInChildren<RangeIndicator>();
        _damageableDetector = gameObject.GetComponent<DamageableDetector>();
        _weaponController = gameObject.GetComponent<WeaponController>();
        _weaponController.enabled = false;
    }
    public void Enable(bool isEnabled)
    {
        enabled = isEnabled;
    }
    public void SetPrice(int price)
    {
        _price = price;
    }
    public void SetTowerDatas(TowersDatas datas)
    {
        _datas = datas;
    }

    private void Update()
    {
        if (_damageableDetector.HasAnyDamageableInRange() == true)
        {
            if (_weaponController.enabled == false)
            {
                _weaponController.enabled = true;
            }

            allTargetedDamageable.Clear();
            foreach (Projectile projectile in _datas.Projectiles)
            {
                allTargetedDamageable.Add(_damageableDetector.GetDamageable(_targetPriority, projectile.ProjectileType.convertProjectileToNightmare()));
            }
            //Damageable targetedDamageable = _damageableDetector.GetDamageable(_targetPriority, _weaponController.getCurrentProjectileType.convertProjectileToNightmare());
            _weaponController.SetTarget(allTargetedDamageable);
        }
        else
        {
            if (_weaponController.enabled == true)
            {
                _weaponController.enabled = false;
            }
        }
    }



    #region DragNDrop Interface & system
    [SerializeField] private List<MeshRenderer> _dragNDropMeshes = null; //For testing as the green/red indicator
    [SerializeField] private List<Collider> _colliders = null; //Enable train and damageable detector colliders after being blaced to prevent weird behaviours
    [SerializeField] private Material _materialGreen = null; //For testing
    [SerializeField] private Material _materialRed = null; //For testing
    [SerializeField] private LayerMask _dragNDroppableLayer;
    [SerializeField] private float _collisionCheckRadius = 2.0f;
    public Transform GetTransform()
    {
        return transform;
    }

    public bool GetIsPlaceable()
    {
        if (Base.Instance.Gold >= _price)
        {
            if (SearchForNearbyBuldings() == false)
            {
                return true;
            }
        }
        return false;
    }

    public void PlaceGhost()
    {
        _weaponController.canShoot = true;
        Enable(true);
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
        Base.Instance.RemoveGold(_price);
    }

    public void EnableDragNDropVFX(bool enable)
    {
        foreach (var meshes in _dragNDropMeshes)
        {
            meshes.enabled = enable;
        }
        _rangeIndicator.EnableRangeIndicator(enable);
    }

    public void SetDragNDropVFXColorToGreen(bool setToGreen)
    {
        if (setToGreen)
        {
            foreach (var meshes in _dragNDropMeshes)
            {
                meshes.material = _materialGreen;
            }
            _rangeIndicator.ChangeIndicatorColor(_materialGreen);
        }
        else
        {
            foreach (var meshes in _dragNDropMeshes)
            {
                meshes.material = _materialRed;
            }
            _rangeIndicator.ChangeIndicatorColor(_materialRed);
        }
    }

    private bool SearchForNearbyBuldings()
    {
        Collider[] colliderList = Physics.OverlapSphere(transform.position, _collisionCheckRadius, _dragNDroppableLayer);
        foreach (var testedCollider in colliderList)
        {
            if (testedCollider.transform.root != transform)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1f);
        Gizmos.DrawWireSphere(transform.position, _collisionCheckRadius);
    }
    #endregion
}
