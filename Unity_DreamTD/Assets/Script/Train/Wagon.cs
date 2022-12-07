using UnityEngine;

//Made By Melinon Remy, modified by ALBERT Esteban to update stats via S.O
public class Wagon : MonoBehaviour
{
    public ProjectileType type;
    public int projectiles;
    private int _maxStorage = 20;

    public bool hasTriggered;

    public int MaxWagonStorage => _maxStorage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Locomotive>() != null)
        {
            hasTriggered = true;
        }
    }

    public void SetMaxStorage(int newMaxStorage)
    {
        _maxStorage = newMaxStorage;
    }
}
