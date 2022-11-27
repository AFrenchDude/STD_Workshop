//By ALBERT Esteban & ALEXANDRE Dorian
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

    private NightmareData.NighmareType _projectileNightmareType;

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

    // Nightmare type Priority functions

    public bool Detect_WeakNightmareType(NightmareData.NighmareType nightmareType)
    {
        foreach (Damageable damageable in _damageablesInRange)
        {
            NightmareManager nightmareManager = damageable.GetComponent<NightmareManager>();

            if (nightmareManager.getNighmareType == nightmareType)
            {
                return true;
            }
        }
        return false;
    }

    public void Focus_WeakNightmareType(NightmareData.NighmareType nightmareType)
    {
        _projectileNightmareType = nightmareType;
    }

    // Damageable functions

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
        bool foundRightType = false;

        Damageable currentDamageable = null;
        float recordDistance = float.MinValue;
        if (gettingNearest)
        {
            recordDistance = float.MaxValue;
        }
        for (int i = 0; i < _damageablesInRange.Count; i++)
        {
            NightmareManager nightmareManager = _damageablesInRange[i].GetComponent<NightmareManager>();

            if (foundRightType == false || nightmareManager.getNighmareType == _projectileNightmareType)
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

                //Test if it was a right nightmare type
                if (nightmareManager.getNighmareType == _projectileNightmareType & foundRightType == false)
                {
                    recordDistance = checkedDistance;
                    currentDamageable = _damageablesInRange[i];
                    foundRightType = true;
                }
            }

        }
        return currentDamageable;
    }

    private Damageable GetLowestHPDamageable()
    {
        bool foundRightType = false;

        Damageable currentDamageable = null;
        int lowestHP = int.MaxValue;

        for (int i = 0; i < _damageablesInRange.Count; i++)
        {
            NightmareManager nightmareManager = _damageablesInRange[i].GetComponent<NightmareManager>();

            if (foundRightType == false || nightmareManager.getNighmareType == _projectileNightmareType)
            {

                int checkedHP = _damageablesInRange[i].CurrentHealth;
                if (checkedHP < lowestHP)
                {
                    lowestHP = checkedHP;
                    currentDamageable = _damageablesInRange[i];
                }
            }

            //Test if it was a right nightmare type
            if (nightmareManager.getNighmareType == _projectileNightmareType & foundRightType == false)
            {
                int checkedHP = _damageablesInRange[i].CurrentHealth;
                lowestHP = checkedHP;

                currentDamageable = _damageablesInRange[i];
                foundRightType = true;
            }
        }
        return currentDamageable;
    }

    private Damageable GetHighestMaxHPDamageable()
    {
        bool foundRightType = false;

        Damageable currentDamageable = null;
        int highestMaxHP = int.MinValue;

        for (int i = 0; i < _damageablesInRange.Count; i++)
        {
            NightmareManager nightmareManager = _damageablesInRange[i].GetComponent<NightmareManager>();

            if (foundRightType == false || nightmareManager.getNighmareType == _projectileNightmareType)
            {

                int checkedHP = _damageablesInRange[i].MaxHP;
                if (checkedHP > highestMaxHP)
                {
                    highestMaxHP = checkedHP;
                    currentDamageable = _damageablesInRange[i];
                }
            }

            //Test if it was a right nightmare type
            if (nightmareManager.getNighmareType == _projectileNightmareType & foundRightType == false)
            {
                int checkedHP = _damageablesInRange[i].CurrentHealth;
                highestMaxHP = checkedHP;

                currentDamageable = _damageablesInRange[i];
                foundRightType = true;
            }

        }
        return currentDamageable;
    }
}
