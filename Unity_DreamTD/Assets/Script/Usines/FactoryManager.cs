//By ALEXANDRE Dorian
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    [SerializeField]
    private FactoryDatas _factoryDatas;

    [Header("References")]

    private Factory _factory;

    [SerializeField]
    private CapsuleCollider _rangeDetector;

    [SerializeField]
    private Transform _centerOfMass;
    public Transform CenterOfMass => _centerOfMass;

    public FactoryDatas FactoryData => _factoryDatas;

    public void OnEnable()
    {
        _factory = GetComponent<Factory>();

        //Apply statistics to every scripts
        ApplyStats(_factoryDatas);
    }

    public void ApplyStats(FactoryDatas factoryData)
    {
        _factoryDatas = Instantiate(factoryData); // Create a new instance for scriptable object

        _factoryDatas.ApplyUpgrade();

        _factory.SetFactoryDatas(_factoryDatas);
            
    }
}
