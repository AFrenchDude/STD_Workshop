using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject info = null;
    [SerializeField]
    private Animator animator = null;

    private void Start()
    {
        info.SetActive(false);
    }

    private void OnMouseOver()
    {
        animator.SetTrigger("Info");
        info.SetActive(true);
    }

    private void OnMouseExit()
    {
        info.SetActive(false);
    }
}
