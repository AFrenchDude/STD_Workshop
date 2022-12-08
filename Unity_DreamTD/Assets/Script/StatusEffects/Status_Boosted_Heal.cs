using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Melinon Remy
public class Status_Boosted_Heal : MonoBehaviour
{
    [SerializeField] private float _slowDuration = 0.1f;
    private Damageable _damageable = null;
    private float _slowStartTime = 0.0f;
    private float _originalMaxHP = 0.0f;
    private float _addedHeal = 5f;

    private void OnEnable()
    {
        _damageable = GetComponentInParent<Damageable>();
        _originalMaxHP = _damageable.MaxHP;
        _slowStartTime = Time.time;
    }
    private void Update()
    {
        if (Time.time >= _slowStartTime + _slowDuration)
        {
            _damageable.setMaxHp(_originalMaxHP, false, true);
            Destroy(this);
        }
    }

    public void ResetTimer()
    {
        _slowStartTime = Time.time;
    }

    public void AddHeal(float addedHeal)
    {
        _addedHeal = addedHeal;
        _damageable.setMaxHp(_originalMaxHP + _addedHeal, false, true);
    }
}
