//By ALBERT Esteban & ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _attack = 1;
    [SerializeField] ProjectileType _attackType;
    [SerializeField] private float _mortarRadius;

    public void SetDamage(float damage)
    {
        _attack = ((int)damage);
    }
    public float getDamage
    {
        get { return _attack; }
    }

    public void SetMortarRadius(float radius)
    {
        _mortarRadius = radius;
    }

    public ProjectileType AttackType => _attackType;
    public UnityEvent<Damageable> DamageDone;
    private void OnTriggerEnter(Collider other)
    {
        Damageable otherDamageable = other.GetComponent<Damageable>();

        if (otherDamageable != null)
        {
            if (GetComponent<AProjectile>().getFireType == TowersDatas.fireType.Mortar)
            {
                Collider[] colliderList = Physics.OverlapSphere(transform.position, _mortarRadius, LayerMask.GetMask("Enemies"));
                
                foreach(Collider collider in colliderList)
                {
                    NightmareData.NighmareType otherNightmareType = otherDamageable.NightmareType;
                    collider.GetComponent<Damageable>().TakeDamage(CheckEffectiveness(otherNightmareType), out float health);
                    DamageDone.Invoke(otherDamageable);
                }
            }
            else
            {
                NightmareData.NighmareType otherNightmareType = otherDamageable.NightmareType;
                otherDamageable.TakeDamage(CheckEffectiveness(otherNightmareType), out float health);             
                DamageDone.Invoke(otherDamageable);

            }
        }
    }

    private float CheckEffectiveness(NightmareData.NighmareType otherNightmareType)
    {
        float newdamage = _attack;

        NightmareData.NighmareType projectileNightmareWeak = _attackType.convertProjectileToNightmare();
        NightmareData.NighmareType projectileNightmareResisted = _attackType.convertProjectileToNightmareResistance();

        

        if (projectileNightmareWeak == otherNightmareType)
        {
            Debug.Log("Strong");
            newdamage = _attack * 1.5f;
        }
        else if (projectileNightmareResisted != otherNightmareType & projectileNightmareResisted != NightmareData.NighmareType.Neutral)
        {
            Debug.Log("Normal");
            newdamage = _attack * 1.25f;
        }
        else
        {
            Debug.Log("Weak");
            newdamage = _attack;
        }

        

        return newdamage;
    }
}
