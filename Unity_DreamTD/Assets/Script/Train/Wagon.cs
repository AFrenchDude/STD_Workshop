using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public Type type;
    public List<GameObject> projectiles;

    public int maxResources = 20;
    
    public void AddRessources(int resourcesToAdd, float secondsToWait)
    {
        StartCoroutine(GetTransfer(resourcesToAdd, secondsToWait));
    }
    
    public void GiveRessources(int resourcesToGive, float secondsToWait, Locomotive locomotive)
    {
        StartCoroutine(GiveTransfer(resourcesToGive, secondsToWait, locomotive));
    }
    
    private IEnumerator GetTransfer(int numberToGive, float secondsToWait)
    {
        if(projectiles.Count != projectiles.Count + numberToGive)
        {
            projectiles.Add(type.projectile);
            numberToGive--;
            yield return new WaitForSeconds(secondsToWait);
            StartCoroutine(GetTransfer(numberToGive, secondsToWait));
        }
    }

    private IEnumerator GiveTransfer(int numberToGive, float secondsToWait, Locomotive locomotive)
    {
        if (numberToGive > 0)
        {
            projectiles.RemoveAt(projectiles.Count - 1);
            numberToGive--;
            yield return new WaitForSeconds(secondsToWait);
            StartCoroutine(GiveTransfer(numberToGive, secondsToWait, locomotive));
        }
        else
        {
            locomotive.isTransferring = false;
        }
    }
}
