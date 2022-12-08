using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent<PointerEventData> OnHover;
    public UnityEvent<PointerEventData> OnUnhover;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        OnHover.Invoke(pointerEventData);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        OnUnhover.Invoke(pointerEventData);
    }

}
