//By ALBERT Esteban
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Effect/PizzaExplosionData", fileName = "PizzaExplosionData")]
public class PizzaEffect_1 : ScriptableObject
{
    [SerializeField] private LayerMask _layerHitbox;
    [SerializeField] private float _explosionRadius = 3.0f;
    [SerializeField] private int _explosionDamage = 5;
    [SerializeField] private GameObject _explosionVFX = null;

    public LayerMask LayerHitbox => _layerHitbox;
    public float ExplosionRadius => _explosionRadius;
    public int ExplosionDamage => _explosionDamage;
    public GameObject ExplosionVFX => _explosionVFX;

}
