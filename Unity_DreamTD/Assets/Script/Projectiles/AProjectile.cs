//By ALBERT Esteban
using UnityEngine;

public abstract class AProjectile : MonoBehaviour
{
    private enum MortarPhase
    {
        up,
        down
    }

    [SerializeField] private bool _destroyOnAttack = true;
    [SerializeField] private float _movementSpeed = 0.0f;

    private TowersDatas.fireType _fireType;
    public TowersDatas.fireType getFireType
    {
        get { return _fireType; }
    }

    [Header("Mortar")]
    [SerializeField]
    private float _heightSwitch = 100f;
    private MortarPhase _mortarPhase = MortarPhase.up;



    public void SetFireType(TowersDatas.fireType fireType)
    {
        _fireType = fireType;
    }
    
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
        if (_fireType == TowersDatas.fireType.Mortar)
        {
            if(_mortarPhase == MortarPhase.up)
            {
                transform.position += transform.forward * _movementSpeed * Time.deltaTime;
            }
            else
            {
                transform.localPosition += transform.forward * _movementSpeed * Time.deltaTime;

                if (_target != null)
                {
                    transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
                }
            }
            

            if(_mortarPhase == MortarPhase.up & transform.position.y > _heightSwitch)
            {
                _mortarPhase = MortarPhase.down;               
            }
        }
        else
        {
            transform.position += transform.forward * _movementSpeed * Time.deltaTime;

            if (_target != null)
            {
                transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
            }
        }
    }

    protected virtual void OnDamageDone(Damageable hitDamageable)
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

    public void SetDestroyOnAttack(bool destroyOnAttack)
    {
        _destroyOnAttack = destroyOnAttack;
    }
}
