using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform _rectTransform;

    public UnityEvent<PointerEventData> OnHover;
    public UnityEvent<PointerEventData> OnUnhover;

    [SerializeField]
    private OnMouseOverAnim _mouseOverAnim;

    [SerializeField]
    private float _animSpeed;

    [SerializeField]
    private AnimationCurve _animCurve;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void ActivateAnimation()
    {
        switch (_mouseOverAnim)
        {
            case (OnMouseOverAnim.ScaleUp):
                StartCoroutine(ScaleAnim(1, 1.1f));               
                break;
            case (OnMouseOverAnim.ScaleDown):
                StartCoroutine(ScaleAnim(1, 0.9f));
                break;
        }

        
    }
    public void ReverseAnimation()
    {
        switch (_mouseOverAnim)
        {
            case (OnMouseOverAnim.ScaleUp):
                StartCoroutine(ScaleAnim(1.1f, 1));
                break;
            case (OnMouseOverAnim.ScaleDown):
                StartCoroutine(ScaleAnim(0.9f, 1f));
                break;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        OnHover.Invoke(pointerEventData);

        if(_mouseOverAnim != OnMouseOverAnim.None)
        {
            ActivateAnimation();
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        OnUnhover.Invoke(pointerEventData);

        if (_mouseOverAnim != OnMouseOverAnim.None)
        {
            ReverseAnimation();
        }
    }


    public IEnumerator ScaleAnim(float StartScale, float ObjectiveScale, float lerp = 0)
    {
        Debug.Log("Anim");
        yield return new WaitForFixedUpdate();
        lerp += Time.deltaTime * _animSpeed;

        _rectTransform.localScale = Mathf.Lerp(StartScale, ObjectiveScale, lerp) * Vector3.one;

        if(lerp >= 1)
        {
            _rectTransform.localScale = ObjectiveScale * Vector3.one;
        }
        else
        {
            StartCoroutine(ScaleAnim(StartScale, ObjectiveScale, lerp));
        }
    }
    

    public enum OnMouseOverAnim
    {
        None,
        ScaleUp,
        ScaleDown
        
    }

}
