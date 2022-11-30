//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Effect/DoTData", fileName = "DoTData")]
public class DoTEffect : ScriptableObject
{
    [SerializeField] private float _dotDuration = 1.0f;
    [SerializeField] private float _tickDamage = 1.0f;
    [SerializeField] private float _tickCD = 1.0f;
    [SerializeField] GameObject _burnVFX = null;

    public float DoTDuration => _dotDuration;
    public float TickDamage => _tickDamage;
    public float TickCD => _tickCD;
    public GameObject BurnVFX => _burnVFX;
}
