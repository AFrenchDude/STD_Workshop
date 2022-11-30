//By ALBERT Esteban
using UnityEngine;
using UnityEngine.Events;

public class Base : Singleton<Base>
{
    [SerializeField] private int _maxBaseHP = 100;
    [SerializeField] private int _startingGold = 50;
    
    [SerializeField]
    private int _currentBaseHP = 1;
    private int _currentGold = 1;

    public int BaseHP => _currentBaseHP;
    public int Gold => _currentGold;

    public UnityEvent BaseDied;
    public UnityEvent<int> GoldUpdated;
    override protected void Awake()
    {
        base.Awake();
        _currentBaseHP = _maxBaseHP;
        _currentGold = _startingGold;
    }

    public void BaseReceiveDMG(int damage)
    {
        _currentBaseHP -= damage;
        if (_currentBaseHP <= 0)
        {
            BaseDied.Invoke();
        }
    }

    public void AddGold(int gold)
    {
        _currentGold += gold;
        GoldUpdated.Invoke(_currentGold);
    }
    public void RemoveGold(int gold)
    {
        _currentGold -= gold;
        GoldUpdated.Invoke(_currentGold);
    }
}