//By ALBERT Esteban, modified by Melinon Remy
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] Camera _camera = null;
    [Header("Movement")]
    [SerializeField] private bool _hasMoveBoundaries = false;
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _minX = 0.0f;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ = 0.0f;
    [SerializeField] private float _maxZ;
    [Header("Zoom")]
    [SerializeField] private float _speedZoom = 4;
    [SerializeField] private int _zoomNumber = 2;
    //player's orientation
    private Vector2 inputValue;
    private Vector3 inputValue3D;
    //Player's zoom
    private Vector2 zoomValue;

    private int _zoomStep;
    private float _zoomCD = 0.05f;
    private float _lastZoomTime = 0.0f;

    private void Awake()
    {
        if (_zoomNumber < 0)
        {
            Debug.LogWarningFormat("Zoom Steps Number ({0}), is inferior to 0, defaulted value to 0", _zoomNumber);
            _zoomNumber = 0;
        }
    }

    private void Update()
    {
        CorrectOOBPosition();
        transform.position += inputValue3D * _speed * Time.deltaTime;
    }

    public void Move(InputAction.CallbackContext obj)
    {
        //when button is pressed
        if (obj.phase == InputActionPhase.Performed)
        {
            inputValue = obj.ReadValue<Vector2>();
            inputValue3D = new Vector3(inputValue.x, 0, inputValue.y);
        }
        //when button is released
        else if (obj.phase == InputActionPhase.Canceled)
        {
            inputValue3D = new Vector3(0, 0, 0);
        }
    }

    public void Zoom(InputAction.CallbackContext obj)
    {
        zoomValue = obj.ReadValue<Vector2>();;
        if (zoomValue.y > 0 && _zoomStep < _zoomNumber && Time.time >= _lastZoomTime + _zoomCD)
        {
            transform.position += _camera.transform.forward * zoomValue.y * _speedZoom;
            _zoomStep++;
            _lastZoomTime = Time.time;
        }
        if (zoomValue.y < 0 && _zoomStep > 0 && Time.time >= _lastZoomTime + _zoomCD)
        {
            transform.position += _camera.transform.forward * zoomValue.y * _speedZoom;
            _zoomStep--;
            _lastZoomTime = Time.time;
        }
    }

    private void CorrectOOBPosition()
    {
        GetIsInMovementBorders(out bool isAtMinX, out bool isAtMaxX, out bool isAtMinZ, out bool isAtMaxZ);
        if (isAtMinX)
        {
            var correctedPosition = transform.position;
            correctedPosition.x = _minX;
            transform.position = correctedPosition;
        }
        if (isAtMaxX)
        {
            var correctedPosition = transform.position;
            correctedPosition.x = _maxX;
            transform.position = correctedPosition;
        }
        if (isAtMinZ)
        {
            var correctedPosition = transform.position;
            correctedPosition.z = _minZ;
            transform.position = correctedPosition;
        }
        if (isAtMaxZ)
        {
            var correctedPosition = transform.position;
            correctedPosition.z = _maxZ;
            transform.position = correctedPosition;
        }
    }

    private void GetIsInMovementBorders(out bool isAtMinX, out bool isAtMaxX, out bool isAtMinZ, out bool isAtMaxZ)
    {
            isAtMinX = false;
            isAtMaxX = false;
            isAtMinZ = false;
            isAtMaxZ = false;

        if (_hasMoveBoundaries)
        {

            if (transform.position.x <= _minX)
            {
                isAtMinX = true;
            }
            if (transform.position.x >= _maxX)
            {
                isAtMaxX = true;
            }
            if (transform.position.z <= _minZ)
            {
                isAtMinZ = true;
            }
            if (transform.position.z >= _maxZ)
            {
                isAtMaxZ = true;
            }
        }
    }
}