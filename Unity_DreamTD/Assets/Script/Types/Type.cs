using UnityEngine;

//Made By Melinon Remy
[CreateAssetMenu(menuName = "Type/Projectile")]
public class Type : ScriptableObject
{
    public enum projectileType
    {
        Food,
        Energy,
        Trap
    }
    public projectileType typeSelected;

    public GameObject projectile;
}
