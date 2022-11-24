//By ALBERT Esteban, for testing purpose
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTurretButton : MonoBehaviour
{
    //[SerializeField] SentryDescription _sentryDescription = null;
    [SerializeField] private Sentry _spawnedSentry = null;

    public void TrySpawnSentry()
    {
        Instantiate(_spawnedSentry);
    }
}
