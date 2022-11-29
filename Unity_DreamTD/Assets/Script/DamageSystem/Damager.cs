//By ALBERT Esteban & ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _attack = 1;
    [SerializeField] ProjectileType _attackType;

    public void SetDamage(float damage)
    {
        _attack = ((int)damage);
    }
    public float getDamage
    {
        get { return _attack; }
    }
    public ProjectileType AttackType => _attackType;
    public UnityEvent<Damageable> DamageDone;
    private void OnTriggerEnter(Collider other)
    {
        Damageable otherDamageable = other.GetComponent<Damageable>();

        if (otherDamageable != null)
        {
            NightmareData.NighmareType otherNightmareType = otherDamageable.NightmareType;
            otherDamageable.TakeDamage(_attack, out float health);
            DamageDone.Invoke(otherDamageable);
        }
    }

    private float CheckEffectiveness(NightmareData.NighmareType otherNightmareType)
    {
        float newdamage = _attack;

        NightmareData.NighmareType projectileNightmareWeak = _attackType.convertProjectileToNightmare();
        NightmareData.NighmareType projectileNightmareresisted = _attackType.convertProjectileToNightmareResistance();
        if (projectileNightmareWeak == otherNightmareType)
        {
            newdamage = _attack * 1.2f;
        }
        if (projectileNightmareresisted == otherNightmareType)
        {
            newdamage = _attack * 0.8f;
        }
        return newdamage;
    }
}
