//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/Effect/GumAreaData", fileName = "GumAreaData")]
public class GumAreaData : ScriptableObject
{
    [SerializeField] GumArea _gumArea = null;
    [SerializeField] private float _lifeTime = 1.0f;
    [SerializeField] private float _slowDuration = 2.0f;
    [Range(0, 1)][SerializeField] private float _slowStrength = 0.5f;

    public GumArea GumAreaPrefab => _gumArea;
    public float GumLifeTime => _lifeTime;
    public float SlowDuration => _slowDuration;
    public float SlowStrength => _slowStrength;
}
