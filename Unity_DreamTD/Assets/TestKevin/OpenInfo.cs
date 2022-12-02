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
        if (info.gameObject.name == "PanelInfoPizza")
        {
            animator.SetBool("PizzaFactory", true);
        }
        else if (info.gameObject.name == "PanelInfoEnergy")
        {
            animator.SetBool("EnergyFactory", true);
        }
        else if (info.gameObject.name == "PanelInfoTraps")
        {
            animator.SetBool("TrapsFactory", true);
        }
    }

    private void OnMouseExit()
    {
        animator.SetBool("PizzaFactory", false);
        animator.SetBool("EnergyFactory", false);
        animator.SetBool("TrapsFactory", false);
    }
}
