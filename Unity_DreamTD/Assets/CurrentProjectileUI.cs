using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentProjectileUI : MonoBehaviour
{
    [SerializeField]
    private Image _iconImage;

    [SerializeField]
    private Image _backgroundImage;

    [SerializeField]
    private Image _ammountImage;

    [SerializeField]
    private TextMeshProUGUI _ammountText;


    private Projectile _projectile;

    private TowersDatas _towerDatas;
    private int _projectileIndex;


    public void KeepReferences(TowersDatas towerData, int projectileIndex)
    {
        _towerDatas = towerData;
        _projectileIndex = projectileIndex;
    }

    public void SetUpProjectile(Projectile projectile)
    {
        _projectile = projectile;
        ApplyProjectileType();
    }

    public void ApplyProjectileType()
    {
        _iconImage.sprite = _projectile.ProjectileType.Icon;

        _backgroundImage.color = _projectile.ProjectileType.ProjectileColor;
        _ammountImage.color = _projectile.ProjectileType.ProjectileColor * new Color(0.5f, 0.5f, 0.5f, 1f);
    }

    [Header("ChangingPanel")]
    [SerializeField]
    private List<ProjectileType> _allProjectiles = new List<ProjectileType>();

    [SerializeField]
    private List<Transform> _changingProjectileParent = new List<Transform>();

    [SerializeField]
    private SelectorTypeOfProjectile _projectileSelectorPrefab;

    public void OpenProjectileTypeSelector()
    {
        int i = 0;
        foreach (ProjectileType projectile in _allProjectiles)
        {
            if (projectile.typeSelected != _projectile.ProjectileType.typeSelected)
            {

                SelectorTypeOfProjectile newProjType = Instantiate(_projectileSelectorPrefab, _changingProjectileParent[i]);
                newProjType.SetProjectileType(projectile, this);

                i++;
            }

            
        }
    }

    public void ChangeProjectile(ProjectileType projectileType)
    {
        _towerDatas.SetProjectileType(_projectileIndex, projectileType);
        SetUpProjectile(_towerDatas.Projectiles[_projectileIndex]);


    }

    private void Update()
    {
        if (_projectile != null)
        {
            _ammountText.text = _projectile.ProjectileAmmount.ToString();
        }
    }
}
