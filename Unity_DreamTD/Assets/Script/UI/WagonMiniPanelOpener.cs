using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WagonMiniPanelOpener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject info = null;

    [SerializeField]
    private Animator animator = null;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OpenInfoPanel();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        CloseInfoPanel();
    }

    public void OpenInfoPanel()
    {
        animator.SetBool("Close", true);
    }

    public void CloseInfoPanel()
    {
        animator.SetBool("Close", false);
    }
}
