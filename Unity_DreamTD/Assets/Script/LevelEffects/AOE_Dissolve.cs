using System.Collections.Generic;
using UnityEngine;

//Made by Alexandre Dorian
public class AOE_Dissolve : MonoBehaviour
{
    private List<Transform> _childs = new List<Transform>();

    [SerializeField]
    private float _delayBeforeDissolve;
    [SerializeField]
    private float _dissolveSpeed = 1;

    private float _startTime;
    private float _lerp = 0;
    private void Awake()
    {

        foreach(Transform child in transform.GetComponentInChildren<Transform>())
        {
            _childs.Add(child);
        }

        _startTime = Time.time;
        
    }

    private void Update()
    {
        if(_startTime + _delayBeforeDissolve < Time.time)
        {           
            _lerp += Time.deltaTime * _dissolveSpeed;

            foreach(Transform child in _childs)
            {
                child.localScale = Vector3.Lerp(child.localScale, Vector3.zero, _lerp);
            }

            if(_lerp >= 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
