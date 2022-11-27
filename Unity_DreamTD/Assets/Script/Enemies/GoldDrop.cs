//By ALBERT Esteban & ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDrop : MonoBehaviour
{
    [SerializeField] private int _goldDropped = 1;

    public void SetGold(int gold)
    {
        _goldDropped = gold;
    }
    private void OnDestroy()
    {
        if (Base.HasInstance)
        {
            Base.Instance.AddGold(_goldDropped);
        }
    }
}
