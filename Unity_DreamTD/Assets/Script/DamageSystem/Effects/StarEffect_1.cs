//By ALBERT Esteban
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Effect/StarBounceData", fileName = "StarBounceData")]
public class StarEffect_1 : ScriptableObject
{
    [SerializeField] private LayerMask _layerHitbox;
    [Range(0.0f, 1.0f)][SerializeField] private float _bounceChance = 0.75f;
    [SerializeField] private int _bounceNumber = 2;
    [SerializeField] private float _bounceRange = 2.0f;
    [Range (0.0f, 1.0f)][SerializeField] private float _stunChance = 0.25f;
    [SerializeField] private float _stunDuration = 1.0f;

    public LayerMask LayerHitBox => _layerHitbox;
    public float BounceChance => _bounceChance;
    public int BounceNumber => _bounceNumber;
    public float BounceRange => _bounceRange;
    public float StunChance => _stunChance;
    public float StunDuration => _stunDuration;

}
