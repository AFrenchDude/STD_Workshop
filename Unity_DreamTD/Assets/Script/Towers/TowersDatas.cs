//By ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DreamTD/TowersData", fileName = "TowersData")]
public class TowersDatas : ScriptableObject
{
    public enum fireType
    {
        Direct,
        DoubleCanon,
        Mortar
    }

    [Header("Stats")]
    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _fireRate;

    [SerializeField]
    private float _range;

    [SerializeField]
    private fireType _fireType;

    /*
    [SerializeField]
    private Type _projectile;

    [SerializeField]
    private int _projectilesAmmount;
    */
    [SerializeField]
    private List<Projectile> _projectileTypeList = new List<Projectile>();

    [SerializeField]
    private int _maxProjectilesAmmount;


    public float Damage => _damage;
    public float FireRate => _fireRate;
    public float Range => _range;
    public fireType FireType => _fireType;
    //public Type Projectile => _projectile;
    //public int ProjectilesAmmount => _projectilesAmmount;

    public List<Projectile> Projectiles => _projectileTypeList;


    public void SetProjAmmount(int index, int ammount)
    {
        _projectileTypeList[index].ProjectileAmmount = ammount;
    }
    public void AddProjAmmount(int index, int ammount)
    {
        _projectileTypeList[index].ProjectileAmmount += ammount;
    }
    public void ReduceProjAmmount(int index, int ammount)
    {
        _projectileTypeList[index].ProjectileAmmount -= ammount;
    }

    //Test if a specific projectil has ammount
    public bool hasProjectiles(int index)
    {
        return _projectileTypeList[index].ProjectileAmmount > 0;
    }




}

[System.Serializable]
public class Projectile
{
    [SerializeField]
    public Type ProjectileType;

    [SerializeField]
    public int ProjectileAmmount;

    [SerializeField]
    public int MaxProjectilesAmmount;
}
