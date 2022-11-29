using UnityEngine;

//Made By Melinon Remy
public class Wagon : MonoBehaviour
{
    public ProjectileType type;
    public int projectiles;
    public int maxResources = 20;

    public bool hasTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Locomotive>() != null)
        {
            hasTriggered = true;
        }
    }
}
