using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareManager : MonoBehaviour
{
    [SerializeField]
    private NightmareData _nightmareData;

    private GoldDrop _goldDrop;
    private PathFollower _pathFollower;
    private Damageable _damageable;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        //Get all references
        _goldDrop = GetComponent<GoldDrop>();
        _pathFollower = GetComponent<PathFollower>();
        _damageable = GetComponent<Damageable>();

        //Set up new Scriptable Object
        _nightmareData = Instantiate(_nightmareData);

        SetUpEnemy();
    }


    public void SetUpEnemy()
    {
        //Debug color
        _meshRenderer.material.color = _nightmareData.debugColor;

        //Hp
        _damageable.setMaxHp(_nightmareData.maxLife);

        //Speed
        _pathFollower.SetSpeed(_nightmareData.speed);

        //gold receive
        _goldDrop.SetGold(_nightmareData.rewardGold);
    }

    public NightmareData.NighmareType getNighmareType
    {
        get { return _nightmareData.nighmareType; }
    }
}
