using UnityEngine;

//Made By Melinon Remy, modified by ALBERT Esteban to update stats via S.O & Modified by Dorian ALEXANDRE to change wagon mesh
public class Wagon : MonoBehaviour
{
    public ProjectileType type;
    public int projectiles;
    private int _maxStorage = 20;

    public bool hasTriggered;

    [SerializeField]
    private Transform _meshParent;

    public int MaxWagonStorage => _maxStorage;

    private void OnEnable()
    {
        SetWagonMesh();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Locomotive>() != null)
        {
            hasTriggered = true;
        }
    }

    public void SetMaxStorage(int newMaxStorage)
    {
        _maxStorage = newMaxStorage;
    }

    public void SetWagonMesh()
    {
        for (int i = 0; i < _meshParent.childCount; i++)
        {
            Destroy(_meshParent.GetChild(i).gameObject);
        }

        Instantiate(type.WagonMesh, _meshParent);
    }
}
