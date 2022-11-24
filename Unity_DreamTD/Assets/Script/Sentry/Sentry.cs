//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : MonoBehaviour,IPickerGhost, ICellChild
{

    [SerializeField] TargetPriority _targetPriority = TargetPriority.Nearest;
    [SerializeField] DamageableDetector _damageableDetector = null;
    [SerializeField] WeaponController _weaponController = null;

    private int _price = 0;

    private BuildableSlot _cellParent = null;

    public int Price => _price;

    private void Awake()
    {
        enabled = false;
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

    // Interfaces
    public Transform GetTransform()
    {
        return transform;
    }

    public bool GetIsPlaceable()
    {
        if (Base.Instance.Gold >= _price)
        {
            return true;
        }
        return false;
    }

    public void OnSetChild()
    {
        Base.Instance.RemoveGold(_price);
        _cellParent = GetComponentInParent<BuildableSlot>();
        Enable(true);
    }
}
