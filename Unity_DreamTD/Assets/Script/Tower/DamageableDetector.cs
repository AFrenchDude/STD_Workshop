//By ALBERT Esteban
using System.Collections.Generic;
using UnityEngine;

public enum TargetPriority
{
    Nearest,
    Furthest,
    LowestHP,
    HighestMaxHP
}

public class DamageableDetector : MonoBehaviour
{
    [System.NonSerialized]
    private List<Damageable> _damageablesInRange = new List<Damageable>();

    public bool HasAnyDamageableInRange()
    {
        return _damageablesInRange.Count > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Damageable damageable = other.GetComponentInParent<Damageable>();

        if (damageable != null && _damageablesInRange.Contains(damageable) == false)
        {
            damageable.Died.RemoveListener(Damageable_OnDied);
            damageable.Died.AddListener(Damageable_OnDied);
            _damageablesInRange.Add(damageable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Damageable damageable = other.GetComponentInParent<Damageable>();

        if (damageable != null && _damageablesInRange.Contains(damageable) == true)
        {
            damageable.Died.RemoveListener(Damageable_OnDied);
            _damageablesInRange.Remove(damageable);
        }
    }

    private void Damageable_OnDied(Damageable caller)
    {
        _damageablesInRange.Remove(caller);
    }

    public Damageable GetDamageable(TargetPriority targetPriority)
    {
        switch (targetPriority)
        {
            case TargetPriority.Nearest:
                return GetNearestOrFurthestDamageable(true);

            case TargetPriority.Furthest:
                return GetNearestOrFurthestDamageable(false);

            case TargetPriority.LowestHP:
                return GetLowestHPDamageable();

            case TargetPriority.HighestMaxHP:
                return GetHighestMaxHPDamageable();

            default:
                return GetNearestOrFurthestDamageable(true);
        }
    }

    private Damageable GetNearestOrFurthestDamageable(bool gettingNearest)
    {
        Damageable currentDamageable = null;
        float recordDistance = float.MinValue;
        if (gettingNearest)
        {
            recordDistance = float.MaxValue;
        }
        for (int i = 0; i < _damageablesInRange.Count; i++)
        {
            float checkedDistance = (_damageablesInRange[i].transform.position - transform.position).magnitude;
            if (gettingNearest)
            {
                if (checkedDistance < recordDistance)
                {
                    recordDistance = checkedDistance;
                    currentDamageable = _damageablesInRange[i];
                }
            }
            else
            {
                if (checkedDistance > recordDistance)
                {
                    recordDistance = checkedDistance;
                    currentDamageable = _damageablesInRange[i];
                }
            }
        }
        return currentDamageable;
    }

    private Damageable GetLowestHPDamageable()
    {
        Damageable currentDamageable = null;
        int lowestHP = int.MaxValue;

        for (int i = 0; i < _damageablesInRange.Count; i++)
        {
            int checkedHP = _damageablesInRange[i].CurrentHealth;
            if (checkedHP < lowestHP)
            {
                lowestHP = checkedHP;
                currentDamageable = _damageablesInRange[i];
            }
        }
        return currentDamageable;
    }

    private Damageable GetHighestMaxHPDamageable()
    {
        Damageable currentDamageable = null;
        int highestMaxHP = int.MinValue;

        for (int i = 0; i < _damageablesInRange.Count; i++)
        {
            int checkedHP = _damageablesInRange[i].MaxHP;
            if (checkedHP > highestMaxHP)
            {
                highestMaxHP = checkedHP;
                currentDamageable = _damageablesInRange[i];
            }
        }
        return currentDamageable;
    }
}
