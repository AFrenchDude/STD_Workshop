//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Base : Singleton<Base>
{
    [SerializeField] private int _maxBaseHP = 100;
    [SerializeField]
    private int _currentBaseHP = 1;

    public int BaseHP => _currentBaseHP;

    public UnityEvent BaseDied;
    override protected void Awake()
    {
        base.Awake();
        _currentBaseHP = _maxBaseHP;
    }

    public void BaseReceiveDMG(int damage)
    {
        _currentBaseHP -= damage;
        if (_currentBaseHP <= 0)
        {
            BaseDied.Invoke();
        }
    }
}