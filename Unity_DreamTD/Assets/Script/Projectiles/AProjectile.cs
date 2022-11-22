//By ALBERT Esteban
using UnityEngine;

public abstract class AProjectile : MonoBehaviour
{

    [SerializeField] private bool _destroyOnAttack = true;
    [SerializeField] private float _movementSpeed = 0.0f;


    protected virtual void OnEnable()
    {
        Damager damagerTested = GetComponent<Damager>();

        if (damagerTested != null)
        {
            damagerTested.DamageDone.RemoveListener(OnDamageDone);
            damagerTested.DamageDone.AddListener(OnDamageDone);
        }
    }
    protected virtual void OnDisable()
    {
        Damager damagerTested = GetComponent<Damager>();

        if (damagerTested != null)
        {
            damagerTested.DamageDone.RemoveListener(OnDamageDone);
        }
    }
    protected virtual void Update()
    {
        transform.position += transform.forward * _movementSpeed * Time.deltaTime;
    }

    protected virtual void OnDamageDone()
    {
        if (_destroyOnAttack)
        {
            Destroy(gameObject);
        }
    }
}
