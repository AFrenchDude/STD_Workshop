//By ALBERT Esteban
using UnityEngine;
using UnityEngine.Events;

public class Base : Singleton<Base>
{
    [SerializeField] private int _maxBaseHP = 100;
    [SerializeField] private int _currentBaseHP = 1;
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
        LevelReferences.Instance.Player.GetComponent<BaseHPManager>().OnLifeChange(_currentBaseHP);
        if (_currentBaseHP <= 0)
        {
            Time.timeScale = 0;
            BaseDied.Invoke();
        }
    }
}
