//By ALBERT Esteban
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DamagerEffect_Star_1 : ADamagerEffect
{
    [SerializeField] StarEffect_1 _starEffectData;
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
                Damageable nearestDamageable = FindNearestDamageable(hitobjects, hitDamageable, false);
                if (nearestDamageable != null)
                {
                    _aprojectileRef.SetTarget(nearestDamageable.transform);
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
            FindNearestDamageable(colliders, hitDamageable, true);
        }
        return nearestDamageable;
    }
}
