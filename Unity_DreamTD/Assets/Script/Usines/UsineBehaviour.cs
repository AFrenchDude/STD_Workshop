using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsineBehaviour : MonoBehaviour
{
    public Type type;
    public List<GameObject> projectiles;

    public int maxRessource = 20;

    private void Start()
    {
        for(var i = 0; i!= maxRessource; i++)
        {
            projectiles.Add(type.projectile);
        }
    }

    public void TransferCoroutine(int numberToGive, int maxWagonCapacity, float secondsToWait, Locomotive locomotive)
    {
        StartCoroutine(Transfer(numberToGive, maxWagonCapacity, secondsToWait, locomotive));
    }

    public IEnumerator Transfer(int numberToGive, int maxWagonCapacity, float secondsToWait, Locomotive locomotive)
    {
        if (numberToGive != maxWagonCapacity || projectiles.Count != 0)
        {
            if(projectiles.Count - 1 >= 0)
            {
                projectiles.RemoveAt(projectiles.Count - 1);
                numberToGive++;
                yield return new WaitForSeconds(secondsToWait);
                StartCoroutine(Transfer(numberToGive, maxWagonCapacity, secondsToWait, locomotive));
            }
            else
            {
                locomotive.usineBehaviour = null;
                locomotive.isTransferring = false;
                locomotive.splineFollower.speed = locomotive.maxSpeed;
                locomotive.SetWagonSpeed();
            }
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
