//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TreeEditor;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] NightmareData.NighmareType _nightmareType = NightmareData.NighmareType.Neutral;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private bool _destroyOnDeath = true;
    [SerializeField] private Transform _targetAnchor = null;
    [SerializeField] private Transform _headAnchor = null;
    public int scoreToGiveOnDeath;
    private float _health = 100;

    [SerializeField]
    private EnemiesHealthBar _healthBar = null;

    public NightmareData.NighmareType NightmareType => _nightmareType;
    public UnityEvent<float> OnDamageTaken;
    public UnityEvent<Damageable> Died;

    public int MaxHP => _maxHealth;
    public float CurrentHealth => _health;
    public Transform TargetAnchor => _targetAnchor;
    public Transform HeadAnchor => _headAnchor;

    public void setMaxHp(float maxHp)
    {
        _maxHealth = (int)maxHp;
        _health = _maxHealth;
    }

    public void TakeDamage(float damage, out float health)
    {
        LevelReferences.Instance.ScoreManager.AddScore((int)damage*10);
        _health -= damage;
        health = _health;
        OnDamageTaken.Invoke(_health);

        if (_health <= 0)
        {
            LevelReferences.Instance.ScoreManager.AddScore(scoreToGiveOnDeath);
            if (_healthBar != null)
            {
                Destroy(_healthBar.gameObject);
            }

            Death();
        }
        else
        {
            if (_healthBar == null)
            {
                UIManager uiManager = LevelReferences.Instance.Player.GetComponent<UIManager>();
                _healthBar = uiManager.CreateEnemiesHealthBar(_headAnchor);
            }

            if (_healthBar != null)
            {
                _healthBar.UpdateLife(_health, _maxHealth);

            }
        }
    }

    private void Death()
    {
        Died.Invoke(this);
        if (_destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }
}
