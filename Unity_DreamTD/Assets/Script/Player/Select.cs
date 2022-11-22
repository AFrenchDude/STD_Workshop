using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Made By Melinon Remy
public class Select : MonoBehaviour
{
    [SerializeField] private LayerMask interactibleLayer;

    public void Selecting(InputAction.CallbackContext obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, interactibleLayer))
        {
            Debug.Log("selected");
        }
    }
}
