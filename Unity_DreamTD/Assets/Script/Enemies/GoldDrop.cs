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
    public void DropGold()
    {
        if (LevelReferences.Instance.Player.GetComponent<GoldManager>() != null)
        {
            LevelReferences.Instance.Player.GetComponent<GoldManager>().CollectMoney(_goldDropped);
        }
    }
}
