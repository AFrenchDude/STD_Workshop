//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private bool _destroyOnDeath = true;
    [SerializeField] private Transform _targetAnchor = null;
    [SerializeField] private int _health = 100;

    public UnityEvent<int> OnDamageTaken;
    public UnityEvent<Damageable> Died;

    public int MaxHP => _maxHealth;
    public int CurrentHealth => _health;
    public Transform TargetAnchor => _targetAnchor;

    public void setMaxHp(float maxHp)
    {
        _maxHealth = (int)maxHp;
        _health = _maxHealth;
    }

    public void TakeDamage(int damage, out int health)
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
