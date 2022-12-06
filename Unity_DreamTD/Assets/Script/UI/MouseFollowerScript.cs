using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowerScript : MonoBehaviour
{
    
    private RectTransform _canvas;
    private RectTransform _rectTransform;


    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = transform.root.GetComponentInChildren<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0)
        {
            _rectTransform.anchoredPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition) * new Vector2(_canvas.sizeDelta.x, _canvas.sizeDelta.y);
        }
    }
}
