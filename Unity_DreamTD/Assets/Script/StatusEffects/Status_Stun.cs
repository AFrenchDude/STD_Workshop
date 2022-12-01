//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Stun : MonoBehaviour
{
    [Range(0, 1)][SerializeField] private float _stunDuration = 2.0f;
    private PathFollower _pathFollower = null;
    private float _stunStartTime = 0.0f;
    private GameObject _stunVFX = null;

    private void OnEnable()
    {
        _pathFollower = GetComponentInParent<PathFollower>();
        _pathFollower.enabled = false;
        _stunStartTime = Time.time;

    }

    private void Update()
    {
        if (Time.time >= _stunStartTime + _stunDuration)
        {
            _pathFollower.enabled = true;
            Destroy(this);
        }
    }

    public void SetStunDuration(float stunDuration)
    {
        _stunDuration = stunDuration;
    }

    public void SetStunVFX(GameObject stunVFX)
    {
        _stunVFX = stunVFX;
        InstantiateVFX();
    }

    public void InstantiateVFX()
    {
        GameObject spawnedVFX = Instantiate(_stunVFX);
        spawnedVFX.transform.position = GetComponentInParent<Damageable>().HeadAnchor.position;
        spawnedVFX.transform.parent = transform;
        Destroy(spawnedVFX, _stunDuration);
    }
}
