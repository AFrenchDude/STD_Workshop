//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiVFXPlay : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshToDissolve = null;
    [SerializeField] private Material _dissolveMat = null;
    [SerializeField] private float _dissolveSpeed = 1.0f;
    [SerializeField] private List<GameObject> _vfxList = null;
    [SerializeField] private List<float> _vfxLifetimes = null;
    [SerializeField] private bool _spawnOnAwake = false;
    private bool _waitingToDissolve = false;
    private float _counter = 0.0f;

    private void Awake()
    {
        if (_spawnOnAwake)
        {
            Dissolve();
        }
    }

    private void Update()
    {
        if (_waitingToDissolve)
        {
            PlayDissolveAnimation();
        }
    }

    [ContextMenu("Dissolve")]
    public void Dissolve()
    {
        SpawnVFXs();
        _meshToDissolve.material = _dissolveMat;
        _waitingToDissolve = true;
    }

    public void SpawnVFXs()
    {
        for (int i = 0; i < _vfxList.Count; i++)
        {
            GameObject vfxSpawned = Instantiate(_vfxList[i]);
            vfxSpawned.transform.position = transform.position;
            Destroy(vfxSpawned, _vfxLifetimes[i]);
        }
    }



    private void PlayDissolveAnimation()
    {
        if (_meshToDissolve.material.GetFloat("_DissolveAmount") < 1)
        {
                _counter += Time.deltaTime * _dissolveSpeed;
                _meshToDissolve.material.SetFloat("_DissolveAmount", _counter);
        }
    }
}
