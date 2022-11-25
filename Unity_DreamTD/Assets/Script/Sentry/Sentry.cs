//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sentry : MonoBehaviour, IPickerGhost
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
    [SerializeField] private GameObject _dragNDropObject = null;
    [SerializeField] private List<Collider> _colliders = null;
    [SerializeField] private Material _materialGreen = null;
    [SerializeField] private Material _materialRed = null;

    [SerializeField] private List<IPickerGhost> _pickeableConflictList = null;
    [SerializeField] private List<Sentry> _sentryConflictList = null;
    public Transform GetTransform()
    {
        return transform;
    }

    public bool GetIsPlaceable()
    {
        if (Base.Instance.Gold >= _price)
        {
            if (_pickeableConflictList == null)
            {
                return true;
            }
            if (_pickeableConflictList.Count <= 0)
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
        _dragNDropObject.SetActive(enable);
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

    private void OnTriggerEnter(Collider other)
    {
        if (_pickeableConflictList == null)
        {
            _pickeableConflictList = new List<IPickerGhost>();
        }
        IPickerGhost otherPickable = other.GetComponentInParent<IPickerGhost>();
        Debug.Log(otherPickable != null);
        if (otherPickable != null)
        {
            _pickeableConflictList.Add(otherPickable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IPickerGhost otherPickable = other.GetComponentInParent<IPickerGhost>();
        if (otherPickable != null)
        {
            _pickeableConflictList.Remove(otherPickable);
        }
    }
    #endregion
}
