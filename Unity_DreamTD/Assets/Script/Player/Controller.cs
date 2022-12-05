//Made by Melinon Remy
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [Tooltip("Default space between player and camera")]
    [SerializeField] private float defaultOffset = -40;
    private CinemachineTransposer transposer;

    [Header("Movement")]
    //player's orientation
    [SerializeField] private float speedMove = 1;
    private Vector2 moveValue;
    private bool isRotating;
    private bool isPressingRightMouse;

    [Header("Zoom")]
    //Player's zoom
    [SerializeField] private float speedZoom = 1;
    [SerializeField] private int zoomNumber = 2;
    private Vector2 zoomValue;
    private bool isZooming;
    private int zoomStep;

    [SerializeField] private PauseBehaviour pauseBehaviour;
    [HideInInspector] public bool isInPause = false;


    private void Awake()
    {
        transposer = virtualCamera.AddCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = new Vector3(0, 0, defaultOffset);
        if (zoomNumber < 0)
        {
            zoomNumber = 0;
        }
    }

    private void Update()
    {
        if (isRotating && !isInPause)
        {
            transform.rotation *= Quaternion.Euler(new Vector3(transform.rotation.x, moveValue.y * speedMove, transform.rotation.z));
        }
        if (isZooming && !isInPause)
        {
            if (zoomValue.y > 0 && zoomStep < zoomNumber)
            {
                zoomStep++;
                transposer.m_FollowOffset += new Vector3(0, 0, zoomValue.y * speedZoom);
            }
            if (zoomValue.y < 0 && zoomStep > 0)
            {
                zoomStep--;
                transposer.m_FollowOffset += new Vector3(0, 0, zoomValue.y * speedZoom);
            }
        }
    }

    public void Move(InputAction.CallbackContext obj)
    {
        //when button is pressed
        if (obj.phase == InputActionPhase.Performed)
        {
            moveValue = obj.ReadValue<Vector2>();
            isRotating = true;
        }
        //when button is released
        else if (obj.phase == InputActionPhase.Canceled)
        {
            isRotating = false;
        }
    }
    public void Zoom(InputAction.CallbackContext obj)
    {
        //when button is pressed
        if (obj.phase == InputActionPhase.Performed)
        {
            zoomValue = obj.ReadValue<Vector2>();
            isZooming = true;
        }
        //when button is released
        else if (obj.phase == InputActionPhase.Canceled)
        {
            isZooming = false;
        }
    }

    public void OnScroll(InputAction.CallbackContext obj)
    {
        zoomValue = obj.ReadValue<Vector2>() / 120;
        if (zoomValue.y > 0 && zoomStep < zoomNumber && !isInPause)
        {
            zoomStep++;
            transposer.m_FollowOffset += new Vector3(0, 0, zoomValue.y * speedZoom);
        }
        if (zoomValue.y < 0 && zoomStep > 0 && !isInPause)
        {
            zoomStep--;
            transposer.m_FollowOffset += new Vector3(0, 0, zoomValue.y * speedZoom);
        }
    }

    public void OnMouseMove(InputAction.CallbackContext obj)
    {
        if (isPressingRightMouse && !isInPause)
        {
            if (obj.ReadValue<Vector2>().x < 0)
            {
                moveValue.y = -1;
                transform.rotation *= Quaternion.Euler(new Vector3(transform.rotation.x, moveValue.y * speedMove, transform.rotation.z));
            }
            else if (obj.ReadValue<Vector2>().x > 0)
            {
                moveValue.y = 1;
                transform.rotation *= Quaternion.Euler(new Vector3(transform.rotation.x, moveValue.y * speedMove, transform.rotation.z));
            }
        }
    }

    public void RightClickPressed(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() == 1)
        {
            isPressingRightMouse = true;
        }
        else if (obj.ReadValue<float>() == 0)
        {
            isPressingRightMouse = false;
        }
    }
    public void Pause(InputAction.CallbackContext obj)
    {
        //when button is pressed
        if (obj.phase == InputActionPhase.Performed)
        {
            AudioSource audio = GetComponent<AudioSource>();
            pauseBehaviour.Pause(audio);
        }
    }
}