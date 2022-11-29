//By ALBERT ESteban
using UnityEngine;

public class DamagerEffect_Pizza_1 : ADamagerEffect
{
    [SerializeField] PizzaEffect_1 _pizzaEffectData;

    public override void DamageEffect(Damageable hitDamageable)
    {
        int hitCount = 0;
        Collider[] hitobjects = Physics.OverlapSphere(transform.position, _pizzaEffectData.ExplosionRadius, _pizzaEffectData.LayerHitbox);
        foreach (var collider in hitobjects)
        {
            Damageable colliderDamageable = collider.GetComponentInParent<Damageable>();
            if (colliderDamageable != null)
            {
                colliderDamageable.TakeDamage(_pizzaEffectData.ExplosionDamage, out float health);
                hitCount++;
            }
        }
        GameObject vfx = Instantiate(_pizzaEffectData.ExplosionVFX);
        vfx.transform.position = transform.position;
        Destroy(vfx, 5.0f);
    }
}
