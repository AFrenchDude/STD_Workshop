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

    [SerializeField]
    private CurrentProjectileUI currentProjectileUI;

    

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
        selectedBackground.GetComponent<Image>().color = new Color(0.9803922f, 0.9176471f, 0.578f,1);
        animator.SetBool("Close", true);
    }

    public void CloseInfoPanel()
    {
        currentProjectileUI.CloseSelector();
        selectedBackground.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        animator.SetBool("Close", false);
    }
}
