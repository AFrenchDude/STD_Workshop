//Dorian ALEXANDRE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockNewProj : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();    
    }

    public void CloseAnimator()
    {
        _animator.SetTrigger("Close");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
