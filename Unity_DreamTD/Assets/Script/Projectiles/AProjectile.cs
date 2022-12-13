//By ALBERT Esteban, mortar functions by ALEXANDRE Dorian
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
    [SerializeField] private GameObject _failSafeTargetPrefab = null;

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
            if (_mortarPhase == MortarPhase.up)
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


            if (_mortarPhase == MortarPhase.up & transform.position.y > _heightSwitch)
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

        if (_fireType == TowersDatas.fireType.Mortar)
        {
            Damageable damageable = _target.root.GetComponentInChildren<Damageable>();
            Debug.Log("Found damageable? " + (damageable != null));
            if (damageable != null)
            {
                damageable.Died.RemoveListener(SetTargetDamageable);
                damageable.Died.AddListener(SetTargetDamageable);
            }

            PathFollower pathFollower = _target.root.GetComponentInChildren<PathFollower>();
            Debug.Log("Found pathfollower? " + (pathFollower != null));
            if (pathFollower != null)
            {
                pathFollower.LastWaypointReached.RemoveListener(SetTargetPathFollower);
                pathFollower.LastWaypointReached.AddListener(SetTargetPathFollower);
            }
        }
    }

    private void SetTargetDamageable(Damageable target) //If Dead then get a new target position
    {
        Debug.Log("SetTargetDamageable");
        PathFollower pathFollower = _target.root.GetComponentInChildren<PathFollower>();
        if (pathFollower != null)
        {
            pathFollower.LastWaypointReached.RemoveListener(SetTargetPathFollower);
        }
        InstantiateFailSafeTarget(target.transform);
    }

    private void SetTargetPathFollower(PathFollower target)//If base reached then get a new target position
    {
        Damageable damageable = _target.root.GetComponentInChildren<Damageable>();
        if (damageable != null)
        {
            damageable.Died.RemoveListener(SetTargetDamageable);
        }
        InstantiateFailSafeTarget(target.transform);
    }

    private void InstantiateFailSafeTarget(Transform target)
    {
        Debug.Log("Instantiate Failsafe");
        GameObject failSafeTarget = Instantiate(_failSafeTargetPrefab);
        failSafeTarget.transform.position = target.transform.position;
        _target = failSafeTarget.transform;
    }

    public void SetDestroyOnAttack(bool destroyOnAttack)
    {
        _destroyOnAttack = destroyOnAttack;
    }
}
