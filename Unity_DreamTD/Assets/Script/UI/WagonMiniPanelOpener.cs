using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WagonMiniPanelOpener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject info = null;

    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private Image selectedBackground = null;

    private void Awake()
    {
        selectedBackground.enabled = false;
    }

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
        selectedBackground.GetComponent<Image>().enabled = true;
        animator.SetBool("Close", true);
    }

    public void CloseInfoPanel()
    {
        selectedBackground.GetComponent<Image>().enabled = false;
        animator.SetBool("Close", false);
    }
}
