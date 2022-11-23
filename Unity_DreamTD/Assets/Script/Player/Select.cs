using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Made By Melinon Remy
public class Select : MonoBehaviour
{
    [SerializeField] private LayerMask interactibleLayer;

    private bool _isdraggingObject = false;
    private GameObject _daggedObject = null;
    public void Selecting(InputAction.CallbackContext obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, interactibleLayer))
        {
            Debug.Log("selected");
        }
    }

    private void Update()
    {
        if (_isdraggingObject)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        }
    }
}
