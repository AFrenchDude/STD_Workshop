//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableToDissolveVFXApplierEnemies : MonoBehaviour
{
    private Damageable _damageable = null;

    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _damageable.SetDestroyOnDeath(false);
    }

    private void OnEnable()
    {
        _damageable.Died.RemoveListener(DissolveEnemy);
        _damageable.Died.AddListener(DissolveEnemy);
    }

    private void OnDisable()
    {
        _damageable.Died.RemoveListener(DissolveEnemy);
    }

    private void DissolveEnemy(Damageable damageable)
    {
        GetComponent<PathFollower>().enabled = false;
        DissolveVFXApplierEnemies dissolveApplier = GetComponent<DissolveVFXApplierEnemies>();
        dissolveApplier.Dissolve();
        Destroy(gameObject, 1 / dissolveApplier.DissolveSpeed); //Division returns exactly how long the animation takes, any way to optimize?
    }
}
