using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

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

    private List<CurrentProjectileUI> _otherProjectilesSelector = new List<CurrentProjectileUI>();

    private Projectile _projectile;

    private TowersDatas _towerDatas;
    private TrainUpgradePanel _trainUpgradePanel = null;
    private int _wagonLinkedIndex;


    private int _projectileIndex;

    private Animator _animator;

    public Projectile Projectile => _projectile;

    public UnityEvent ProjectileCreated;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void KeepReferences(TowersDatas towerData, int projectileIndex)
    {
        _towerDatas = towerData;
        _projectileIndex = projectileIndex;
    }

    public void SetUpProjectile(Projectile projectile)
    {
        _projectile = projectile;
        ProjectileCreated.Invoke();
        ApplyProjectileType();
    }

    public void SetUpProjectile(ProjectileType type, int number, int maxAmmount)
    {
        Projectile createdProjectile = new Projectile();
        createdProjectile.SetupProjectile(type, number, maxAmmount);
        SetUpProjectile(createdProjectile);
    }

    public void SetRefToTrainPanel(TrainUpgradePanel trainUpgradePanel, int wagonIndex)
    {
        _trainUpgradePanel = trainUpgradePanel;
        _wagonLinkedIndex = wagonIndex;
    }

    public void SetOtherProjectilesPreview(List<CurrentProjectileUI> listOtherProjectiles)
    {
        _otherProjectilesSelector = listOtherProjectiles;
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

    private List<SelectorTypeOfProjectile> _projTypeList = new List<SelectorTypeOfProjectile>();

    public void OpenProjectileTypeSelector()
    {
        if (_projTypeList.Count == 0)
        {

            foreach(CurrentProjectileUI currentProjectileUI in _otherProjectilesSelector)
            {
                if(currentProjectileUI != this)
                {
                    currentProjectileUI.CloseSelector();
                }
            }

            int i = 0;
            foreach (ProjectileType projectile in _allProjectiles)
            {
                if (projectile.typeSelected != _projectile.ProjectileType.typeSelected)
                {

                    SelectorTypeOfProjectile newProjType = Instantiate(_projectileSelectorPrefab, _changingProjectileParent[i]);
                    newProjType.SetProjectileType(projectile, this);
                    _projTypeList.Add(newProjType);

                    i++;
                }


            }

            _animator.SetBool("Open", true);
        }
        else
        {
            _animator.SetBool("Open", false);
        }
    }

    public void CloseSelector()
    {
        _animator.SetBool("Open", false);
    }

    public void ChangeProjectile(ProjectileType projectileType)
    {
        if(_towerDatas != null)
        {
            _towerDatas.SetProjectileType(_projectileIndex, projectileType);
            SetUpProjectile(_towerDatas.Projectiles[_projectileIndex]);
        }
        else if(_trainUpgradePanel != null)
        {
            _trainUpgradePanel.SetNewProjectileType(_wagonLinkedIndex, projectileType); 
        }



        _animator.SetBool("Open", false);
    }

    public void DestroyAllSelectorType()
    {
        foreach(SelectorTypeOfProjectile selector in _projTypeList)
        {
            Destroy(selector.gameObject);
        }
        _projTypeList.Clear();
    }

    private void Update()
    {
        if (_projectile != null)
        {
            _ammountText.text = _projectile.ProjectileAmmount.ToString();
        }
    }
}
