using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInformationManager : MonoBehaviour
{
    private Animator _animator;

    private bool _isWavePreviewOpen;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isWavePreviewOpen = _animator.GetBool("Open");
    }

    public void SwitchWavesPreview()
    {
        _isWavePreviewOpen = !_isWavePreviewOpen;
        _animator.SetBool("Open", _isWavePreviewOpen);
    }

    public void CreateWavePreview()
    {

    }
}
