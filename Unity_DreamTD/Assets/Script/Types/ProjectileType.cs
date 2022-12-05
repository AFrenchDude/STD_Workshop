using System.Collections.Generic;
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

    public UnityEngine.GameObject projectile;

    public Sprite icon;

    public List<AudioClip> shotSound;

    public NightmareData.NighmareType convertProjectileToNightmare()
    {
        switch (typeSelected)
        {
            case (projectileType.Neutral):
                return NightmareData.NighmareType.Neutral;

            case (projectileType.Food):
                return NightmareData.NighmareType.Vegetables;

            case (projectileType.Energy):
                return NightmareData.NighmareType.Skeleton;

            case (projectileType.Trap):
                return NightmareData.NighmareType.Insects;
        }

        return NightmareData.NighmareType.Neutral;
    }

    public NightmareData.NighmareType convertProjectileToNightmareResistance()
    {
        switch (typeSelected)
        {
            case (projectileType.Neutral):
                return NightmareData.NighmareType.Neutral;

            case (projectileType.Food):
                return NightmareData.NighmareType.Insects;

            case (projectileType.Energy):
                return NightmareData.NighmareType.Vegetables;

            case (projectileType.Trap):
                return NightmareData.NighmareType.Skeleton;
        }

        return NightmareData.NighmareType.Neutral;
    }
}
