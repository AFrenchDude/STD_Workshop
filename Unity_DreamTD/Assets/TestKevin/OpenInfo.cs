// By DIJOUX Kevin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject info = null;

    [SerializeField]
    private Animator animator = null;

    private void Start()
    {
        info.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OpenInfoPanel();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        CloseInfoPanel();
    }

    private void OpenInfoPanel()
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

    private void CloseInfoPanel()
    {
        animator.SetBool("InfoPizza", false);
        animator.SetBool("InfoEnergy", false);
        animator.SetBool("InfoTraps", false);
    }
}
