//By ALEXANDRE Dorian
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField]
    private TowersDatas _towersDatas;

    [Header("References")]

    private Tower _tower;
    private WeaponController _weaponController;

    [SerializeField]
    private CapsuleCollider _rangeDetector;

    public TowersDatas TowersData => _towersDatas;

    private void OnEnable()
    {
        _tower = GetComponent<Tower>();
        _weaponController = GetComponent<WeaponController>();

        //Apply statistics to every scripts
        ApplyStats(_towersDatas);
    }

    public void ApplyStats(TowersDatas towerData)
    {
        _towersDatas = Instantiate(towerData); // Create a new instance for scriptable object

        _towersDatas.ApplyUpgrade();

        _weaponController.setTowerData(_towersDatas);
        _tower.SetTowerDatas(_towersDatas);
        _rangeDetector.radius = _towersDatas.Range;
    }
}
