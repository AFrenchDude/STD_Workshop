using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Made By Melinon Remy
public class UsineBehaviour : MonoBehaviour
{
    public ProjectileType type;
    public int projectiles;
    public int maxRessource = 20;
    public bool isProducing = true;
    public float cooldown = 1;
    private float lastProduction;

    //Production
    private void Update()
    {
        if(type.typeSelected.ToString() != "None" && projectiles < maxRessource && Time.time > lastProduction + cooldown && isProducing)
        {
            projectiles++;
            lastProduction = Time.time;
        }
    }
}
