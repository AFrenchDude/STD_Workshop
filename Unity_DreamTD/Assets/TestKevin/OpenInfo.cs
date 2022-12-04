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

    private void CloseInfoPanel()
    {
        animator.SetBool("InfoPizzaTower", false);
        animator.SetBool("InfoEnergyTower", false);
        animator.SetBool("InfoTrapsTower", false);
    }
}
