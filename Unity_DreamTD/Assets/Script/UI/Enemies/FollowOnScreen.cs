using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOnScreen : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private RectTransform _rectTransform;
    private Camera _camera;
    private Canvas _canvas;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = transform.parent.parent.GetComponent<Canvas>();
        _camera = LevelReferences.Instance.UICamera;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            Vector2 myPositionOnScreen = Camera.main.WorldToScreenPoint(_target.position);
            float scaleFactor = _canvas.scaleFactor;

            _rectTransform.anchoredPosition = new Vector2(myPositionOnScreen.x / scaleFactor, myPositionOnScreen.y / scaleFactor);
            
        }
    }
}