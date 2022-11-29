//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] NightmareData.NighmareType _nightmareType = NightmareData.NighmareType.Neutral;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private bool _destroyOnDeath = true;
    [SerializeField] private Transform _targetAnchor = null;
    private float _health = 100;

    public NightmareData.NighmareType NightmareType => _nightmareType;
    public UnityEvent<float> OnDamageTaken;
    public UnityEvent<Damageable> Died;

    public int MaxHP => _maxHealth;
    public float CurrentHealth => _health;
    public Transform TargetAnchor => _targetAnchor;

    public void setMaxHp(float maxHp)
    {
        _maxHealth = (int)maxHp;
        _health = _maxHealth;
    }

    public void TakeDamage(float damage, out float health)
    {
        _health -= damage;
        health = _health;
        OnDamageTaken.Invoke(_health);

        if (_health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Died.Invoke(this);
        if (_destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }
}
