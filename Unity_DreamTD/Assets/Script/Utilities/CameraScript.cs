//By ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private float _length;
    [SerializeField]
    private float _height;

    [SerializeField]
    private Vector2 _minMaxFOVByScroll;

    [SerializeField]
    private Vector2 _minMaxHeightByDrag;

    [SerializeField]
    private float _cameraAngle;
    [SerializeField]
    private float _mouseSensibility;
    [SerializeField]
    private float _scrollSensibility;

    [SerializeField]
    private Vector3 _offset;

    private bool _isPaused = false;

    // Update is called once per frame
    void Update()
    {
            this.transform.position = _target.transform.position + Quaternion.Euler(0, _cameraAngle, 0) * (new Vector3(0, _height, -_length));
            this.transform.rotation = Quaternion.LookRotation((_target.transform.position - this.transform.position).normalized) * Quaternion.Euler(_offset);

            if (Input.GetMouseButton(1))
            {
                _cameraAngle += Input.GetAxis("Mouse X") * _mouseSensibility;

                if (Input.GetAxis("Mouse Y") > 0 & _height >= _minMaxHeightByDrag.x)
                {
                    _height -= Input.GetAxis("Mouse Y") * _mouseSensibility;
                }
                else if (Input.GetAxis("Mouse Y") < 0 & _height <= _minMaxHeightByDrag.y)
                {
                    _height -= Input.GetAxis("Mouse Y") * _mouseSensibility;
                }

                if (_height >= _minMaxHeightByDrag.y)
                {
                    _height = _minMaxHeightByDrag.y;
                }
                else if (_height <= _minMaxHeightByDrag.x)
                {
                    _height = _minMaxHeightByDrag.x;
                }
            }

            if (_cameraAngle >= 360)
            {
                _cameraAngle = 0;
            }

            if (Input.mouseScrollDelta.y < 0 & Camera.main.fieldOfView < _minMaxFOVByScroll.y)
            {
                Camera.main.fieldOfView -= Input.mouseScrollDelta.y * 0.1f * _scrollSensibility;
            }
            else if (Input.mouseScrollDelta.y > 0 & Camera.main.fieldOfView > _minMaxFOVByScroll.x)
            {
                Camera.main.fieldOfView -= Input.mouseScrollDelta.y * 0.1f * _scrollSensibility;
            }
    }
}
