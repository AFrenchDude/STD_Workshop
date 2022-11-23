//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDrop : MonoBehaviour
{
    [SerializeField] private int _goldDropped = 1;

    private void OnDestroy()
    {
        if (Base.HasInstance)
        {
            Base.Instance.AddGold(_goldDropped);
        }
    }
}
