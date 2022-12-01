using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Info UI", fileName = "Info Button Shop")]
public class Info : ScriptableObject
{
    [SerializeField] public Sprite sprite = null;
    [SerializeField] public new string name = null;
    [SerializeField] public string type = null;
    [SerializeField] public float production = 0f;
    [SerializeField] public int capacity = 0;
    [SerializeField] public Sprite moneySprite = null;
    [SerializeField] public int moneyNecessary = 0;
}
