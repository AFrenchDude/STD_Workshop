//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Tower : MonoBehaviour, IPickerGhost
{

    [SerializeField] TargetPriority _targetPriority = TargetPriority.Nearest;
    [SerializeField] DamageableDetector _damageableDetector = null;
    [SerializeField] WeaponController _weaponController = null;

    private int _price = 0;


    public int Price => _price;

    private void Awake()
    {
        Enable(false);
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

    #region DragNDrop Interface & system
    [SerializeField] private GameObject _dragNDropObject = null; //For testing as the green/red indicator
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
        Enable(true);
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
        Base.Instance.RemoveGold(_price);
    }

    public void EnableDragNDropVFX(bool enable)
    {
        _dragNDropObject.GetComponent<MeshRenderer>().enabled = enable;
    }

    public void SetDragNDropVFXColorToGreen(bool setToGreen)
    {
        if (setToGreen)
        {
            _dragNDropObject.GetComponent<MeshRenderer>().material = _materialGreen;

        }
        else
        {
            _dragNDropObject.GetComponent<MeshRenderer>().material = _materialRed;
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
