//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveVFXApplierObjects : MonoBehaviour
{
    [SerializeField] private Material _dissolveMat = null;
    [SerializeField] private float _dissolveSpeed = 1.0f;
    [SerializeField] private List<GameObject> _vfxList = null;
    [SerializeField] private List<float> _vfxLifetimes = null;
    [SerializeField] private bool _spawnOnAwake = false;
    private bool _waitingToDissolve = false;
    private float _counter = 0.0f;
    private List<MeshRenderer> _meshRendererList = new List<MeshRenderer>();

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
        SetDissolveMaterial();
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

    private void SetDissolveMaterial()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            MeshRenderer checkedMeshRenderer = child.GetComponent<MeshRenderer>();
            if (checkedMeshRenderer != null)
            {
                _meshRendererList.Add(checkedMeshRenderer);
                Material[] materialArrayCopy = checkedMeshRenderer.materials;
                for (int i = 0; i < materialArrayCopy.Length; i++)
                {
                    Texture2D originalMainTexture = materialArrayCopy[i].GetTexture("_MainTex") as Texture2D;
                    var dissolveMapInstance = Instantiate(_dissolveMat);
                    dissolveMapInstance.SetTexture("_MainTexture", originalMainTexture);
                    materialArrayCopy[i] = dissolveMapInstance;
                }
                checkedMeshRenderer.materials = materialArrayCopy;
            }
            //Debug.Break();
        }
    }

    private void PlayDissolveAnimation()
    {
        foreach (var meshRenderer in _meshRendererList)
        {
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                if (meshRenderer.materials[i].GetFloat("_DissolveAmount") < 1)
                {
                    meshRenderer.materials[i].SetFloat("_DissolveAmount", _counter);
                }
            }
        }
        if (_counter < 1)
        {
            _counter += Time.deltaTime * _dissolveSpeed;
        }
    }
}
