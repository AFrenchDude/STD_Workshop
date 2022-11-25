using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made By Melinon Remy
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
    /*
    public void TransferCoroutine(int numberToGive, float secondsToWait, Locomotive locomotive)
    {
        if (projectiles.Count > numberToGive)
        {
            StartCoroutine(Transfer(numberToGive, secondsToWait, locomotive));
        }
        else
        {
            StartCoroutine(Transfer(projectiles.Count, secondsToWait, locomotive));
        }
    }

    public IEnumerator Transfer(int numberToGive, float secondsToWait, Locomotive locomotive)
    {
        if (numberToGive != 0)
        {
            numberToGive--;
            projectiles.RemoveAt(projectiles.Count - 1);
            yield return new WaitForSeconds(secondsToWait);
            StartCoroutine(Transfer(numberToGive, secondsToWait, locomotive));
        }
        else
        {
            if (locomotive.wagons[locomotive.index + 1] != null)
            {
                //locomotive.SetUsine(locomotive.wagons[locomotive.index + 1], this);
            }
            else
            {
                locomotive.index = 0;
            }
        }
    }*/
}
