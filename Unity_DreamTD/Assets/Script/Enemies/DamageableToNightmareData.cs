//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableToNightmareData : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<Damageable>().Died.RemoveListener(AddKillCountToNightmareData);
        GetComponent<Damageable>().Died.AddListener(AddKillCountToNightmareData);
    }
    private void OnDisable()
    {
        GetComponent<Damageable>().Died.RemoveListener(AddKillCountToNightmareData);
    }

    private void AddKillCountToNightmareData(Damageable damageable)
    {
        GetComponent<NightmareManager>().OriginalNightmareData.AddKillCount();
    }
}
