using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static NightmareData;

public class NightmareManager : MonoBehaviour
{
    [SerializeField]
    private NightmareData _nightmareData;

    private GoldDrop _goldDrop;
    private PathFollower _pathFollower;
    private Damageable _damageable;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    private List<GameObject> boostedEnemies;

    private enum SupportEffect
    {
        None,
        Heal,
        Speed,
        Resist
    }

    [SerializeField] private SupportEffect supportType;
    [SerializeField] private float health;
    [SerializeField] private float speed;

    private void Awake()
    {
        //Get all references
        _goldDrop = GetComponent<GoldDrop>();
        _pathFollower = GetComponent<PathFollower>();
        _damageable = GetComponent<Damageable>();

        if (_nightmareData.nightmareFunction == NightmareData.NightmareFunction.Support)
        {

        }
    }

    public void SetEnemyData(NightmareData nightmareData)
    {
        _nightmareData = nightmareData;

        //Set up new Scriptable Object
        _nightmareData = Instantiate(_nightmareData);

        SetUpEnemy();
    }

    public void SetUpEnemy()
    {
        //Debug color
        if (_meshRenderer != null)
        {

            _meshRenderer.material.color = _nightmareData.debugColor;
        }

        //Hp
        _damageable.setMaxHp(_nightmareData.maxLife, true, true);

        //Speed
        _pathFollower.SetSpeed(_nightmareData.speed);
        _pathFollower.SetOriginalSpeed(_nightmareData.speed);

        //gold receive
        _goldDrop.SetGold(_nightmareData.rewardGold);
    }

    public NightmareData.NighmareType getNighmareType
    {
        get { return _nightmareData.nighmareType; }
    }

    void Update()
    {
        if (_nightmareData.nightmareFunction == NightmareFunction.Support)
        {
            Collider[] colliderList = Physics.OverlapSphere(transform.position, 8);
            foreach (var testedCollider in colliderList)
            {
                if (testedCollider.transform.gameObject.layer == gameObject.layer && testedCollider.transform.gameObject != transform.gameObject)
                {
                    if (testedCollider.transform.GetComponent<NightmareManager>() != null && testedCollider.transform.GetComponent<NightmareManager>().getNighmareType == _nightmareData.getNightmareType)
                    {
                        switch (supportType)
                        {
                            case SupportEffect.None:
                                break;

                            case SupportEffect.Heal:
                                if(testedCollider.GetComponent<Status_Boosted_Heal>() == null)
                                {
                                    testedCollider.AddComponent<Status_Boosted_Heal>();
                                    testedCollider.GetComponent<Status_Boosted_Heal>().AddHeal(_nightmareData.Boost);
                                }
                                else
                                {
                                    testedCollider.GetComponent<Status_Boosted_Heal>().ResetTimer();
                                }
                                break;

                            case SupportEffect.Speed:
                                if (testedCollider.GetComponent<Status_Boosted_Speed>() == null)
                                {
                                    testedCollider.AddComponent<Status_Boosted_Speed>();
                                    testedCollider.GetComponent<Status_Boosted_Speed>().AddSpeed(_nightmareData.Boost);
                                }
                                else
                                {
                                    testedCollider.GetComponent<Status_Boosted_Speed>().ResetTimer();
                                }
                                break;

                            case SupportEffect.Resist:
                                //Increase resistance
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_nightmareData.nightmareFunction == NightmareFunction.Support)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 8);
        }
    }
}
