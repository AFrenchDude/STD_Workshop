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

    public FactoryDatas getFactoryData
    {
        get { return _factoryDatas; }
    }


    public void SetPrice(int price)
    {
        _price = price;
    }

    //Production
    private void Update()
    {
        bool conditionType = _factoryDatas.ProjectileType.typeSelected.ToString() != "None";
        bool conditionSpace = _factoryDatas.Ammount < _factoryDatas.MaxAmmount;
        bool conditionTime = Time.time > lastProduction + _factoryDatas.ProductionRate;

        if (conditionType && conditionSpace && conditionTime && _factoryDatas.IsProducing)
        {
            _factoryDatas.AddProjectile(1);
            lastProduction = Time.time;
        }
    }
    public void Enable(bool isEnabled)
    {
        _factoryDatas = Instantiate(_factoryDatas);
        _factoryDatas.SetProductionEnable(isEnabled);
        enabled = isEnabled;
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
    }
    public Transform GetTransform()
    {
        return transform;
    }

    public bool GetIsPlaceable()
    {
        if (Base.Instance.Gold >= _price)
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
        Base.Instance.RemoveGold(_price);
    }

    public void EnableDragNDropVFX(bool enable)
    {
        if (enable)
        {
            foreach (var meshes in _dragNDropMeshes)
            {
                originalMaterial = meshes.material;
            }         
        }
        else
        {
            foreach (var meshes in _dragNDropMeshes)
            {
                meshes.material = originalMaterial;
            }
            
        }

        if (_parentMeshRenderers != null)
        {

            _parentMeshRenderers.GetComponent<Animator>().SetBool("Activated", !enable);
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
