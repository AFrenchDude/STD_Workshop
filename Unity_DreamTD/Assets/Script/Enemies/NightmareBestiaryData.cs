//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/NightmareBestiaryData", fileName = "NightmareBestiaryData")]
public class NightmareBestiaryData : ScriptableObject
{
    [SerializeField] string _name = "Unknown";
    [SerializeField] NightmareData _nightmareData = null;
    [SerializeField] string _description = "Unknown";
    [SerializeField] GameObject _enemyPrefab = null;
    private bool _bestiaryDataUnlocked = false;


    public string Name => _name;
    public NightmareData NightmareData => _nightmareData;
    public string Description => _description;
    public GameObject EnemyModel => _enemyPrefab;
    public bool IsUnlocked => _bestiaryDataUnlocked;

    public void UnlockBestiaryData()
    {
        _bestiaryDataUnlocked = true;
    }
    public void ResetStats()
    {
        _nightmareData.ResetKillCount();
        _bestiaryDataUnlocked = false;
    }
}
