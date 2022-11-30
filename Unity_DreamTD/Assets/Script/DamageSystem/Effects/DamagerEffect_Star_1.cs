//By ALBERT Esteban
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DamagerEffect_Star_1 : ADamagerEffect
{
    [SerializeField] StarEffect_1 _starEffectData;
    [SerializeField] ProjectileUpgradeData.EnergyUpgrades _projectileUpgrade = ProjectileUpgradeData.EnergyUpgrades.Bouncing;
    private int _bounceDone = 0;
    private AProjectile _aprojectileRef = null;
    private List<Damageable> _previousDamageable = new List<Damageable>();

    private void Start()
    {
        _aprojectileRef = GetComponentInParent<AProjectile>();
        if (_starEffectData.BounceNumber > 0)
        {
            _aprojectileRef.SetDestroyOnAttack(false);
        }
    }
    //private void Update()
    //{
    //    if (true)
    //    {

    //    }
    //}
    public override void DamageEffect(Damageable hitDamageable)
    {
        
        float diceRoll = Random.Range(0f, 1f);
        
        if (diceRoll <= _starEffectData.StunChance)
        {
            Status_Stun entityStatusStun = hitDamageable.GetComponentInParent<Status_Stun>();
            if (entityStatusStun == null)
            {
                Status_Stun currententityStatusSlow = hitDamageable.gameObject.AddComponent<Status_Stun>();
                currententityStatusSlow.SetStunDuration(_starEffectData.StunDuration);
            }
        }
        if (diceRoll <= _starEffectData.BounceChance)
        {
            _bounceDone++;
            if (_bounceDone <= _starEffectData.BounceNumber)
            {
                Collider[] hitobjects = Physics.OverlapSphere(transform.position, _starEffectData.BounceRange, _starEffectData.LayerHitBox);
                Damageable nearestDamageable = TryFindNearestDamageable(hitobjects, hitDamageable);
                if (nearestDamageable != null)
                {
                    _aprojectileRef.SetTarget(nearestDamageable.TargetAnchor);
                    _previousDamageable.Add(hitDamageable);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            if (_bounceDone >= _starEffectData.BounceNumber)
            {
                _aprojectileRef.SetDestroyOnAttack(true);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Damageable TryFindNearestDamageable(Collider[] colliders, Damageable hitDamageable)
    {
        Damageable targetedDamageable = null;
        targetedDamageable = FindNearestDamageable(colliders, hitDamageable, false);
        if (targetedDamageable == null)
        {
            targetedDamageable = FindNearestDamageable(colliders, hitDamageable, true);
        }
        return targetedDamageable;
    }

    private Damageable FindNearestDamageable(Collider[] colliders, Damageable hitDamageable, bool isSecondRun)
    {
        Damageable nearestDamageable = null;
        float recordDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            Damageable checkedDamageable = collider.GetComponentInParent<Damageable>();
            if (checkedDamageable != null && checkedDamageable != hitDamageable && _previousDamageable.Contains(checkedDamageable) == false)
            {
                float checkedDistance = (checkedDamageable.transform.position - transform.position).magnitude;
                if (checkedDistance < recordDistance)
                {
                    nearestDamageable = checkedDamageable;
                    recordDistance = checkedDistance;
                }
            }
        }
        if (nearestDamageable == null && isSecondRun == false)
        {
            _previousDamageable.Clear();
        }
        return nearestDamageable;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1f);
        Gizmos.DrawWireSphere(transform.position, _starEffectData.BounceRange);
    }

    public override ProjectileUpgradeData.EnergyUpgrades GetEnergyUpgradeValue()
    {
        return _projectileUpgrade;
    }
}