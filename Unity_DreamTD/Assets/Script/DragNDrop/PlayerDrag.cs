//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;

public class PlayerDrag : MonoBehaviour
{
    [SerializeField] private LayerMask _interactibleLayer;
    [SerializeField] private float _spherecastRadius = 3.0f;

    private IPickerGhost _ghost = null;
    private bool _isDragging = false;

    public void Selecting(InputAction.CallbackContext obj)
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, float.MaxValue, interactibleLayer))
        //{
        //    Debug.Log("selected");
        //}
    }

    private void Update()
    {
        if (_isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasFoundSurface = Physics.Raycast(ray, out RaycastHit cursorHit, float.MaxValue, _interactibleLayer);
            if (hasFoundSurface)
            {
                RaycastHit[] splinePoints = Physics.SphereCastAll(cursorHit.transform.position, _spherecastRadius, cursorHit.transform.position, 500.0f, _interactibleLayer);

                Transform nearestSplinePoint = GetNearestSplinePoint(cursorHit, splinePoints);

                if (nearestSplinePoint != null)
                {
                    Vector3 slotPosition;
                    slotPosition = nearestSplinePoint.position + (cursorHit.transform.position - nearestSplinePoint.transform.position).normalized * 3.0f;
                    _ghost.GetTransform().position = slotPosition;
                    _ghost.GetTransform().LookAt(nearestSplinePoint);
                }
                else
                {
                    _ghost.GetTransform().position = cursorHit.transform.position;
                }
            }



            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            //_ghost.GetTransform().position = _gridPicker.HitPosition;
        }
    }

    private static Transform GetNearestSplinePoint(RaycastHit cursorHit, RaycastHit[] splinePoints)
    {
        float nearestDistance = float.MaxValue;
        Transform nearestSplinePoint = null;

        for (int i = 0; i < splinePoints.Count(); i++)
        {
            if (splinePoints[i].transform != null)
            {
                float checkedDistance = (cursorHit.transform.position - splinePoints[i].transform.position).magnitude;
                if (checkedDistance < nearestDistance)
                {
                    nearestDistance = checkedDistance;
                    nearestSplinePoint = splinePoints[i].transform;
                }
            }
        }
        return nearestSplinePoint;
    }

    public void ActivateWithGhost(IPickerGhost ghost)
    {
        _ghost = ghost;
        ActivateDrag(true);
    }
    public void ActivateDrag(bool enable)
    {
        _isDragging = enable;
    }
    public void DestroyDraggedItem()
    {
        if (_ghost != null)
        {
            Destroy(_ghost.GetTransform().gameObject);
            _ghost = null;
        }
    }


    [ContextMenu("Activate")]
    private void DoActivate() => ActivateDrag(true);

    [ContextMenu("Deactivate")]
    private void DoDeactivate() => ActivateDrag(false);
}
