//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamager : MonoBehaviour
{
    [SerializeField] int _damageToBase = 1;

    public void DamageBase()
    {
        Base.Instance.BaseReceiveDMG(_damageToBase);
    }
}
