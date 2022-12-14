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

    [SerializeField]
    private bool _isUnlock = true;

    private void Start()
    {
        info.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isUnlock)
        {
            OpenInfoPanel();
        }
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isUnlock)
        {
            CloseInfoPanel();
        }    
    }

    private void OpenInfoPanel()
    {
        if (info.gameObject.name == "InfoSnipeTower" || info.gameObject.name == "InfoPizzaFactory")
        {
            animator.SetBool("InfoPizza", true);
        }
        else if (info.gameObject.name == "InfoMortarTower" || info.gameObject.name == "InfoEnergyFactory")
        {
            animator.SetBool("InfoEnergy", true);
        }
        else if (info.gameObject.name == "InfoCanonTower" || info.gameObject.name == "InfoTrapsFactory")
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
