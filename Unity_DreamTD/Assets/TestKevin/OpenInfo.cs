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
        if (info.gameObject.name == "InfoPizzaTower" || info.gameObject.name == "InfoPizzaFactory")
        {
            animator.SetBool("InfoPizza", true);
        }
        else if (info.gameObject.name == "InfoEnergyTower" || info.gameObject.name == "InfoEnergyFactory")
        {
            animator.SetBool("InfoEnergy", true);
        }
        else if (info.gameObject.name == "InfoTrapsTower" || info.gameObject.name == "InfoTrapsFactory")
        {
            animator.SetBool("InfoTraps", true);
        }
    }

    private void OnMouseExit()
    {
        animator.SetBool("InfoPizza", false);
        animator.SetBool("InfoEnergy", false);
        animator.SetBool("InfoTraps", false);
    }
}
