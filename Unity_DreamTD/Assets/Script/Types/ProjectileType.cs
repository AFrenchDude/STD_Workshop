using UnityEngine;

//Made By Melinon Remy
[CreateAssetMenu(menuName = "Type/Projectile")]
public class ProjectileType : ScriptableObject
{
    public enum projectileType
    {
        Neutral,
        Food,
        Energy,
        Trap
    }
    public projectileType typeSelected;

    public GameObject projectile;

    public Sprite icon;

    public NightmareData.NighmareType convertProjectileToNightmare()
    {
        switch (typeSelected)
        {
            case (projectileType.Neutral):
                return NightmareData.NighmareType.Vegetables;

            case (projectileType.Food):
                return NightmareData.NighmareType.Vegetables;

            case (projectileType.Energy):
                return NightmareData.NighmareType.Skeleton;

            case (projectileType.Trap):
                return NightmareData.NighmareType.Insects;
                
        }

        return NightmareData.NighmareType.Vegetables;
    }
}
