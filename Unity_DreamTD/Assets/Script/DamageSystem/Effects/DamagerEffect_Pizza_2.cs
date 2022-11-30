//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerEffect_Pizza_2 : ADamagerEffect
{
    [SerializeField] private DoTEffect _dotEffectData = null;
    [SerializeField] ProjectileUpgradeData.FoodUpgrades _projectileUpgrade = ProjectileUpgradeData.FoodUpgrades.SpicyHotPizza;

    public override void DamageEffect(Damageable hitDamageable)
    {
        Status_DoT entityStatusDoT = hitDamageable.GetComponentInParent<Status_DoT>();
        if (entityStatusDoT != null)
        {
            entityStatusDoT.ResetTimer();
        }
        else
        {
            Status_DoT currentEntityStatusDoT = hitDamageable.gameObject.AddComponent<Status_DoT>();
            currentEntityStatusDoT.SetDoTDuration(_dotEffectData.DoTDuration);
            currentEntityStatusDoT.SetTickDamage(_dotEffectData.TickDamage);
            currentEntityStatusDoT.SetTickCD(_dotEffectData.TickCD);
            currentEntityStatusDoT.SetDoTVFX(_dotEffectData.BurnVFX);
        }
    }
    public override ProjectileUpgradeData.FoodUpgrades GetFoodUpgradeValue()
    {
        return _projectileUpgrade;
    }
}
