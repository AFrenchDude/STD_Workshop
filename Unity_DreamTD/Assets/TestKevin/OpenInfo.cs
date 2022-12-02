// By DIJOUX Kevin
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
        info.SetActive(true);
    }

    private void OnMouseOver()
    {
        if (info.gameObject.name == "InfoPizzaTower")
        {
            animator.SetBool("InfoPizzaTower", true);
        }
        else if (info.gameObject.name == "InfoEnergyTower")
        {
            animator.SetBool("InfoEnergyTower", true);
        }
        else if (info.gameObject.name == "InfoTrapsTower")
        {
            animator.SetBool("InfoTrapsTower", true);
        }
    }

    private void OnMouseExit()
    {
        animator.SetBool("InfoPizzaTower", false);
        animator.SetBool("InfoEnergyTower", false);
        animator.SetBool("InfoTrapsTower", false);
    }
}
