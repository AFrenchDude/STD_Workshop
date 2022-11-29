//By ALBERT Esteban
using UnityEngine;

public class DamagerEffect_Gum_1 : ADamagerEffect
{
    [SerializeField] GumAreaData _gumAreaData = null;
    [SerializeField] ProjectileUpgradeData.TrapUpgrades _projectileUpgrade = ProjectileUpgradeData.TrapUpgrades.GumArea;
    public override void DamageEffect(Damageable hitDamageable)
    {
        GumArea gumArea = Instantiate(_gumAreaData.GumAreaPrefab);
        gumArea.SetGumAreaData(_gumAreaData);
        gumArea.transform.position = hitDamageable.transform.position;
    }

    public override ProjectileUpgradeData.TrapUpgrades GetTrapUpgradeValue()
    {
        return _projectileUpgrade;
    }
}

