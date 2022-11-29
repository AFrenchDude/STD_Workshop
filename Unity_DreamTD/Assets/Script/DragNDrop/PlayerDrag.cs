//By ALBERT Esteban
using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    [SerializeField] private LayerMask _interactibleLayer;
    [SerializeField] private float _snapDetectionRange = 5.0f;
    [SerializeField] private float _buildToRailDistance = 3.0f;

    private IPickerGhost _ghost = null;
    private bool _isDragging = false;
    private bool _isSnappedToRail = false;

    private void Update()
    {
        if (_isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasFoundSurface = Physics.Raycast(ray, out RaycastHit cursorHit, float.MaxValue, _interactibleLayer);
            BuildingDragNDrop(hasFoundSurface, cursorHit);
        }
    }

    private void BuildingDragNDrop(bool hasFoundSurface, RaycastHit cursorHit)
    {
        if (hasFoundSurface)
        {
            if (LevelReferences.HasInstance)
            {
                SplineDone splineRefTest = LevelReferences.Instance.RailSpline;
                SplineDone.Point nearestSplinePoint = splineRefTest.GetClosestPoint(cursorHit.point);
                if ((nearestSplinePoint.position - cursorHit.point).magnitude <= _snapDetectionRange)
                {
                    Vector3 splinePointForward = LevelReferences.Instance.RailSpline.GetForwardAt(nearestSplinePoint.t);
                    Vector3 towerSnapDirection = Vector3.Cross(Vector3.up, splinePointForward).normalized;
                    Vector3 splinePointToCursorHit = cursorHit.point - nearestSplinePoint.position;

                    if (Vector3.Dot(towerSnapDirection, splinePointToCursorHit) < 0)
                    {
                        towerSnapDirection = towerSnapDirection * (-1);
                    }

                    Vector3 towerSnapVector = nearestSplinePoint.position + towerSnapDirection * _buildToRailDistance;


                    SnapDraggedItemToRail(towerSnapVector, nearestSplinePoint);
                    if (_ghost.GetIsPlaceable() == true)
                    {
                        _ghost.SetDragNDropVFXColorToGreen(true);
                    }
                    else
                    {
                        _ghost.SetDragNDropVFXColorToGreen(false);
                    }
                    return;
                }
            }
            _ghost.GetTransform().position = cursorHit.point;
            _ghost.GetTransform().rotation = Quaternion.Euler(0, 0, 0);
            _ghost.SetDragNDropVFXColorToGreen(false);
            _isSnappedToRail = false;
        }
    }


    private void SnapDraggedItemToRail(Vector3 towerSnapLocation, SplineDone.Point nearestSplinePoint)
    {
        _ghost.GetTransform().position = towerSnapLocation;
        _ghost.GetTransform().LookAt(nearestSplinePoint.position);
        _isSnappedToRail = true;
    }
    public void ActivateWithGhost(IPickerGhost ghost)
    {
        _ghost = ghost;
        EnableDragnDropGhostVFX(true);
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

    public bool TrySetBuildingInAction()
    {
        if (_isSnappedToRail && _ghost.GetIsPlaceable())
        {
            _ghost.PlaceGhost();
            EnableDragnDropGhostVFX(false);
            _ghost = null;
            return true;
        }
        return false;
    }
    public void EnableDragnDropGhostVFX(bool enable)
    {
        _ghost.EnableDragNDropVFX(enable);
    }

    [ContextMenu("Activate")]
    private void DoActivate() => ActivateDrag(true);

    [ContextMenu("Deactivate")]
    private void DoDeactivate() => ActivateDrag(false);
}
