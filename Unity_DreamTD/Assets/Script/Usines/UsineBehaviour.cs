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
}
