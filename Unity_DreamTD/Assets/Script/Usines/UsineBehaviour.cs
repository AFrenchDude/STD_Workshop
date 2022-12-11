using System.Collections.Generic;
using UnityEngine;

//Made By Melinon Remy
public class UsineBehaviour : MonoBehaviour, IPickerGhost
{
    [SerializeField]
    private FactoryDatas _factoryDatas;
    private int _price = 0;

    private float lastProduction;
    public int Price => _price;
    private Material originalMaterial;

    private AudioSource audioSource;
    [SerializeField] private int scoreToGiveOnProducing;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public FactoryDatas getFactoryData
    {
        get { return _factoryDatas; }
    }


    public void SetPrice(int price)
    {
        _price = price;
    }

    [SerializeField]
    private FactoryManagerPanel _associatedFactoryManager;
    public void SetFactoryManagerPanel(FactoryManagerPanel factoryPanel)
    {
        _associatedFactoryManager = factoryPanel;
    }


    //Production
    private void Update()
    {
        bool conditionType = _factoryDatas.ProjectileType.typeSelected.ToString() != "None";
        bool conditionSpace = _factoryDatas.Ammount < _factoryDatas.MaxAmmount;
        bool conditionTime = Time.time > lastProduction + _factoryDatas.ProductionRate;
        bool conditionWave = LevelReferences.Instance.SpawnerManager.isWaveRunning;

        if (conditionType && conditionSpace && conditionWave && conditionTime && _factoryDatas.IsProducing)
        {
            LevelReferences.Instance.ScoreManager.AddScore(scoreToGiveOnProducing);
            _factoryDatas.AddProjectile(1);
            lastProduction = Time.time;

            if(_associatedFactoryManager != null)
            {
                _associatedFactoryManager.SetProjectileContained(_factoryDatas);
            }
        }
    }
    public void Enable(bool isEnabled)
    {
        _factoryDatas = GetComponent<FactoryManager>().FactoryData;
        
        _factoryDatas.SetProductionEnable(isEnabled);
        enabled = isEnabled;
        ActiveAnimation(true);
        if (isEnabled)
        {
            audioSource.Play();
        }
    }

    #region DragNDrop Interface & system
    [Header("Personnalize")]
    [SerializeField] private Transform _parentMeshRenderers = null;
    [SerializeField] private List<MeshRenderer> _dragNDropMeshes = null; //For testing as the green/red indicator
    [SerializeField] private List<Collider> _colliders = null; //Enable train and damageable detector colliders after being blaced to prevent weird behaviours
    [SerializeField] private Material _materialGreen = null; //For testing
    [SerializeField] private Material _materialRed = null; //For testing
    [SerializeField] private LayerMask _dragNDroppableLayer;
    [SerializeField] private float _collisionCheckRadius = 2.0f;

    private GoldManager _goldManager;

    private void Awake()
    {
        if (_parentMeshRenderers != null)
        {
            if (_parentMeshRenderers.childCount > 0)
            {
                _dragNDropMeshes.Clear();
                for (int i = 0; i < _parentMeshRenderers.childCount; i++)
                {
                    Transform mesh = _parentMeshRenderers.GetChild(i);
                    if (mesh.GetComponent<MeshRenderer>() != null)
                    {
                        _dragNDropMeshes.Add(mesh.GetComponent<MeshRenderer>());
                    }
                }
            }
        }

        _goldManager = LevelReferences.Instance.Player.GetComponent<GoldManager>();
    }

    public void SetUpgradeMesh(GameObject mesh)
    {
        Destroy(_parentMeshRenderers.gameObject);
        _parentMeshRenderers = Instantiate(mesh, this.transform).transform;
        _parentMeshRenderers.GetComponent<Animator>().SetBool("Activated", true);


    }

    public void ActiveAnimation(bool activted)
    {
        _parentMeshRenderers.GetComponent<Animator>().SetBool("Activated", activted);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool GetIsPlaceable()
    {
        if (_goldManager.CanBuy(_price))
        {
            if (SearchForNearbyBuldings() == false)
            {
                return true;
            }
        }
        return false;
    }

    public void PlaceGhost()
    {
        _factoryDatas.SetProductionEnable(true);
        Enable(true);
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
        string objectName = _factoryDatas.name + "_Create_Lvl0";
        _goldManager.Buy(_price, objectName);
    }

    public void EnableDragNDropVFX(bool enable)
    {
        if (enable)
        {

        }
        else
        {
            Destroy(_parentMeshRenderers.gameObject);
            SetUpgradeMesh(_factoryDatas.CurrentUpgrade.UpgradePrefab);

        }

        if (_parentMeshRenderers != null)
        {
            if (gameObject.GetComponent<Animator>() != null)
            {
                _parentMeshRenderers.GetComponent<Animator>().SetBool("Activated", !enable);

            }
        }
    }

    public void SetDragNDropVFXColorToGreen(bool setToGreen)
    {
        if (setToGreen)
        {
            foreach (var meshes in _dragNDropMeshes)
            {
                meshes.material = _materialGreen;
            }
        }
        else
        {
            foreach (var meshes in _dragNDropMeshes)
            {
                meshes.material = _materialRed;
            }
        }
    }
    private bool SearchForNearbyBuldings()
    {
        Collider[] colliderList = Physics.OverlapSphere(transform.position, _collisionCheckRadius, _dragNDroppableLayer);
        foreach (var testedCollider in colliderList)
        {
            if (testedCollider.transform.root != transform)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
