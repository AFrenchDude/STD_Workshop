//By ALBERT Esteban & ALEXANDRE Dorian & MELINON Remy
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _attack = 1;
    [SerializeField] ProjectileType _attackType;
    [SerializeField] private float _mortarRadius;
    [SerializeField] private TowersDatas.fireType _fireType;

    //Equilibrium
    private bool _miraculousBullet = false;
    public void ActiveMiraculousBullet()
    {
        if (_attackType.typeSelected != ProjectileType.projectileType.Neutral)
        {
            _miraculousBullet = true;
        }

    }

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
    public void SetFireType(TowersDatas.fireType fire)
    {
        _fireType = fire;
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

                //Particles instantiate
                GameObject particles = Instantiate(_attackType.HitAOE);
                Vector3 height = new Vector3(0, 0.2f, 0);

                particles.transform.position = otherDamageable.transform.position + height;


                foreach (Collider collider in colliderList)
                {
                    NightmareData.NighmareType otherNightmareType = otherDamageable.NightmareType;
                    collider.GetComponent<Damageable>().TakeDamage(CheckEffectiveness(otherNightmareType, otherDamageable), out float health);
                    DamageDone.Invoke(otherDamageable);
                }

            }
            else
            {
                NightmareData.NighmareType otherNightmareType = otherDamageable.NightmareType;
                otherDamageable.TakeDamage(CheckEffectiveness(otherNightmareType, otherDamageable), out float health);

                //Particles instantiate
                GameObject particles = Instantiate(_attackType.HitFX);

                particles.transform.position = transform.position;
                particles.transform.rotation = transform.rotation;

                DamageDone.Invoke(otherDamageable);

            }
        }
    }

    private float CheckEffectiveness(NightmareData.NighmareType otherNightmareType, Damageable enemyDamageable)
    {
        float newdamage = _attack;

        NightmareData.NighmareType projectileNightmareWeak = _attackType.convertProjectileToNightmare();
        NightmareData.NighmareType projectileNightmareResisted = _attackType.convertProjectileToNightmareResistance();

        float highBoost = 2f;
        float normalBoost = 1.25f;
        float debuffBoost = .75f;

        switch (_fireType)
        {
            case (TowersDatas.fireType.Direct):
                highBoost = 1.5f;
                normalBoost = 1f;
                debuffBoost = 1f;
                break;


            case (TowersDatas.fireType.Mortar):
                highBoost = 1.75f;
                normalBoost = 1f;
                debuffBoost = .75f;
                break;

            case (TowersDatas.fireType.DoubleCanon):
                highBoost = 2f;
                normalBoost = 1.25f;
                debuffBoost = .75f;
                break;
        }


        if (projectileNightmareWeak == otherNightmareType)
        {
            Debug.Log("Strong");
            newdamage = _attack * highBoost;
        }
        else if (projectileNightmareResisted != otherNightmareType & projectileNightmareResisted != NightmareData.NighmareType.Neutral)
        {
            Debug.Log("Normal");
            newdamage = _attack * normalBoost;
        }
        else
        {
            Debug.Log("Weak");
            newdamage = _attack * debuffBoost;
        }

        //Test if it's last projectile
        if (_miraculousBullet)
        {
            newdamage = newdamage + 1f;
        }


        return newdamage;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, _mortarRadius);
    }
}
