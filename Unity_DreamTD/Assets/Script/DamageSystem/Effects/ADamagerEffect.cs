//By ALBERT Esteban
using UnityEngine;

public abstract class ADamagerEffect : MonoBehaviour
{
    private Damager _damager = null;

    private void Awake()
    {
        _damager = GetComponent<Damager>();
        if (_damager == null)
        {
            throw new System.Exception("ADamagerEffect on an object with no Damager");
        }
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


    public abstract void DamageEffect(Damageable hitDamageable);

}
