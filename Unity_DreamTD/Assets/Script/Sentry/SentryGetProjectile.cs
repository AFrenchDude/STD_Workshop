using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryGetProjectile : MonoBehaviour
{
    public Type type;
    public List<GameObject> projectiles;

    public int maxRessource = 20;

    public void TransferCoroutine(int numberToGive, float secondsToWait, Locomotive locomotive)
    {
        StartCoroutine(Transfer(numberToGive, secondsToWait, locomotive));
    }

    public IEnumerator Transfer(int numberToGive, float secondsToWait, Locomotive locomotive)
    {
        if (projectiles.Count != numberToGive || projectiles.Count != maxRessource)
        {
            projectiles.Add(type.projectile);
            yield return new WaitForSeconds(secondsToWait);
            StartCoroutine(Transfer(numberToGive, secondsToWait, locomotive));
        }
        else
        {
            locomotive.usineBehaviour = null;
            locomotive.isTransferring = false;
            locomotive.splineFollower.speed = locomotive.maxSpeed;
            locomotive.SetWagonSpeed();
        }
    }
}
