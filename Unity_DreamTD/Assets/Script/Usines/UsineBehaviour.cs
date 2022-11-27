using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Made By Melinon Remy
public class UsineBehaviour : MonoBehaviour
{
    public Type type;
    public List<GameObject> projectiles;
    public int maxRessource = 20;
    public bool isProducing = true;

    public float cooldown = 1;
    private float lastProduction;

    private void Update()
    {
        if(type.typeSelected.ToString() != "None" && projectiles.Count < maxRessource && Time.time > lastProduction + cooldown && isProducing)
        {
            projectiles.Add(type.projectile);
            lastProduction = Time.time;
        }
    }
}
