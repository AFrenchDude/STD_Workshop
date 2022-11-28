//By ALBERT Esteban
using UnityEngine;

public abstract class ADamagerEffect : MonoBehaviour
{
    [SerializeField] ProjectileType _projectileType = null;
    private Damager _damager = null;

    private void Awake()
    {
        _damager = GetComponent<Damager>();
    }
    private void OnEnable()
    {
        _damager.DamageDone.RemoveListener(DamageEffect);
        _damager.DamageDone.AddListener(DamageEffect);
    }
    private void OnDisable()
    {
        _damager.DamageDone.RemoveListener(DamageEffect);
    }


    public abstract void DamageEffect();
}
