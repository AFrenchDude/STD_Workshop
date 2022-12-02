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

    //private NightmareData.NighmareType _projectileNightmareType;

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
        //Debug.Log("Remove " + other.gameObject);
        Damageable damageable = other.GetComponentInParent<Damageable>();

        if (damageable != null && _damageablesInRange.Contains(damageable) == true)
        {
            damageable.Died.RemoveListener(Damageable_OnDied);
            _damageablesInRange.Remove(damageable);
        }
    }


    // Damageable functions
    private void Damageable_OnDied(Damageable caller)
    {
        _damageablesInRange.Remove(caller);
    }

    public Damageable GetDamageable(TargetPriority targetPriority, NightmareData.NighmareType projectileNightmareType)
    {
        switch (targetPriority)
        {
            case TargetPriority.Nearest:
                return GetNearestOrFurthestDamageable(true, projectileNightmareType);

            case TargetPriority.Furthest:
                return GetNearestOrFurthestDamageable(false, projectileNightmareType);

            case TargetPriority.LowestHP:
                return GetLowestHPDamageable(projectileNightmareType);

            case TargetPriority.HighestMaxHP:
                return GetHighestMaxHPDamageable(projectileNightmareType);

            default:
                return GetNearestOrFurthestDamageable(true, projectileNightmareType);
        }
    }

    private Damageable GetNearestOrFurthestDamageable(bool gettingNearest, NightmareData.NighmareType projectileNightmareType)
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

            if (foundRightType == false || nightmareManager.getNighmareType == projectileNightmareType || projectileNightmareType == NightmareData.NighmareType.Neutral)
            {
                float checkedDistance = (_damageablesInRange[i].transform.position - transform.position).sqrMagnitude;
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
                if (nightmareManager.getNighmareType == projectileNightmareType & foundRightType == false)
                {
                    recordDistance = checkedDistance;
                    currentDamageable = _damageablesInRange[i];
                    foundRightType = true;
                }
            }

        }
        return currentDamageable;
    }

    private Damageable GetLowestHPDamageable(NightmareData.NighmareType projectileNightmareType)
    {
        bool foundRightType = false;

        Damageable currentDamageable = null;
        float lowestHP = float.MaxValue;

        for (int i = 0; i < _damageablesInRange.Count; i++)
        {
            NightmareManager nightmareManager = _damageablesInRange[i].GetComponent<NightmareManager>();

            if (foundRightType == false || nightmareManager.getNighmareType == projectileNightmareType)
            {

                float checkedHP = _damageablesInRange[i].CurrentHealth;
                if (checkedHP < lowestHP)
                {
                    lowestHP = checkedHP;
                    currentDamageable = _damageablesInRange[i];
                }
            }

            //Test if it was a right nightmare type
            if (nightmareManager.getNighmareType == projectileNightmareType & foundRightType == false)
            {
                float checkedHP = _damageablesInRange[i].CurrentHealth;
                lowestHP = checkedHP;

                currentDamageable = _damageablesInRange[i];
                foundRightType = true;
            }
        }
        return currentDamageable;
    }

    private Damageable GetHighestMaxHPDamageable(NightmareData.NighmareType projectileNightmareType)
    {
        bool foundRightType = false;

        Damageable currentDamageable = null;
        float highestMaxHP = float.MinValue;

        for (int i = 0; i < _damageablesInRange.Count; i++)
        {
            NightmareManager nightmareManager = _damageablesInRange[i].GetComponent<NightmareManager>();

            if (foundRightType == false || nightmareManager.getNighmareType == projectileNightmareType)
            {

                float checkedHP = _damageablesInRange[i].MaxHP;
                if (checkedHP > highestMaxHP)
                {
                    highestMaxHP = checkedHP;
                    currentDamageable = _damageablesInRange[i];
                }
            }

            //Test if it was a right nightmare type
            if (nightmareManager.getNighmareType == projectileNightmareType & foundRightType == false)
            {
                float checkedHP = _damageablesInRange[i].CurrentHealth;
                highestMaxHP = checkedHP;

                currentDamageable = _damageablesInRange[i];
                foundRightType = true;
            }

        }
        return currentDamageable;
    }
}
