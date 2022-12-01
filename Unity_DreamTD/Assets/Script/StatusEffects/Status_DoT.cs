//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_DoT : MonoBehaviour
{
    private Damageable _damageable = null;
    private float _dotDuration = 1.0f;
    private float _dotStartTime = 0.0f;
    private float _tickDamage = 1.0f;
    private float _tickCD = 1.0f;
    private float _lastTickTime = 0.0f;
    private GameObject _dotVFX = null;


    private void OnEnable()
    {
        _damageable = GetComponentInParent<Damageable>();
        _dotStartTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >= _lastTickTime + _tickCD)
        {
            _damageable.TakeDamage(_tickDamage, out float health);
            GameObject spawnedVFX = Instantiate(_dotVFX);
            spawnedVFX.transform.position = transform.position;
            spawnedVFX.transform.parent = transform;
            Destroy(spawnedVFX, 5.0f);
            _lastTickTime = Time.time;
        }
        if (Time.time >= _dotStartTime + _dotDuration)
        {
            Destroy(this);
        }
    }

    public void ResetTimer()
    {
        _dotStartTime = Time.time;
    }
    public void SetDoTDuration(float duration)
    {
        _dotDuration = duration;
    }
    public void SetTickDamage(float damage)
    {
        _tickDamage = damage;
    }
    public void SetTickCD(float tickCD)
    {
        _tickCD = tickCD;
    }
    public void SetDoTVFX(GameObject dotVFX)
    {
        _dotVFX = dotVFX;
    }
}
