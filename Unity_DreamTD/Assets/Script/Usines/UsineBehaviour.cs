using System.Collections.Generic;
using UnityEngine;

//Made By Melinon Remy
public class UsineBehaviour : MonoBehaviour, IPickerGhost
{
    public ProjectileType type;
    public int projectiles;
    public int maxRessource = 20;
    public bool isProducing = false;
    public float cooldown = 1;
    private float lastProduction;
    private int _price = 0;
    public int Price => _price;
    private Material originalMaterial;

    public void SetPrice(int price)
    {
        _price = price;
    }

    //Production
    private void Update()
    {
        if(type.typeSelected.ToString() != "None" && projectiles < maxRessource && Time.time > lastProduction + cooldown && isProducing)
        {
            projectiles++;
            lastProduction = Time.time;
        }
    }
    public void Enable(bool isEnabled)
    {
        enabled = isEnabled;
    }

    #region DragNDrop Interface & system
    [SerializeField] private List<MeshRenderer> _dragNDropMeshes = null; //For testing as the green/red indicator
    [SerializeField] private List<Collider> _colliders = null; //Enable train and damageable detector colliders after being blaced to prevent weird behaviours
    [SerializeField] private Material _materialGreen = null; //For testing
    [SerializeField] private Material _materialRed = null; //For testing
    [SerializeField] private LayerMask _dragNDroppableLayer;
    [SerializeField] private float _collisionCheckRadius = 2.0f;
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
        isProducing = true;
        Enable(true);
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
        Base.Instance.RemoveGold(_price);
    }
    
    public void EnableDragNDropVFX(bool enable)
    {
        if(enable)
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
