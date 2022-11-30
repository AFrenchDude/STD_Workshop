//By ALBERT Esteban
using UnityEngine;

public abstract class ADamagerEffect : MonoBehaviour, UpgradeComponent
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

    //Interface
    public virtual ProjectileUpgradeData.NeutralUpgrades GetNeutralUpgradeValue()
    {
        ProjectileUpgradeData.NeutralUpgrades baseReturn = ProjectileUpgradeData.NeutralUpgrades.Basic;
        return baseReturn;
    }
    public virtual ProjectileUpgradeData.EnergyUpgrades GetEnergyUpgradeValue()
    {
        ProjectileUpgradeData.EnergyUpgrades baseReturn = ProjectileUpgradeData.EnergyUpgrades.Basic;
        return baseReturn;
    }
    public virtual ProjectileUpgradeData.FoodUpgrades GetFoodUpgradeValue()
    {
        ProjectileUpgradeData.FoodUpgrades baseReturn = ProjectileUpgradeData.FoodUpgrades.Basic;
        return baseReturn;
    }
    public virtual ProjectileUpgradeData.TrapUpgrades GetTrapUpgradeValue()
    {
        ProjectileUpgradeData.TrapUpgrades baseReturn = ProjectileUpgradeData.TrapUpgrades.Basic;
        return baseReturn;
    }
}
