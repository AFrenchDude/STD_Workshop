//By ALBERT Esteban
using UnityEngine;

public class DamagerEffect_Gum_1 : ADamagerEffect
{
    [SerializeField] GumAreaData _gumAreaData = null;
    public override void DamageEffect(Damageable hitDamageable)
    {
        GumArea gumArea = Instantiate(_gumAreaData.GumAreaPrefab);
        gumArea.SetGumAreaData(_gumAreaData);
        gumArea.transform.position = transform.position;
    }
}

