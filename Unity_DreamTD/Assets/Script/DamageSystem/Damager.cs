//By ALBERT Esteban & ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _attack = 1;

    public void SetDamage(float damage)
    {
        _attack = ((int)damage);
    }
    public int getDamage
    {
        get { return _attack; }
    }

    public UnityEvent DamageDone;
    private void OnTriggerEnter(Collider other)
    {
        Damageable otherDamageable = other.GetComponent<Damageable>();

        if (otherDamageable != null)
        {
            otherDamageable.TakeDamage(_attack, out int health);
            DamageDone.Invoke();
        }
    }
}
