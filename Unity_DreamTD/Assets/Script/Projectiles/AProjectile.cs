//By ALBERT Esteban
using UnityEngine;

public abstract class AProjectile : MonoBehaviour
{

    [SerializeField] private bool _destroyOnAttack = true;
    [SerializeField] private float _movementSpeed = 0.0f;

    private Transform _target;

    public void SetSpeed(float speed)
    {
        _movementSpeed = speed;
    }

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

        if (_target != null)
        {
            Vector3 targetPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);
            transform.rotation = Quaternion.LookRotation(targetPos - transform.position);
        }
    }

    protected virtual void OnDamageDone()
    {
        if (_destroyOnAttack)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
