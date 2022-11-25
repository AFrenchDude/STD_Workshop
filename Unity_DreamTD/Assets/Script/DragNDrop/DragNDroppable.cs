//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class DragNDroppable : MonoBehaviour//, IPickerGhost
{

    //[SerializeField] private GameObject _dragNDropObject = null;
    //[SerializeField] private List<Collider> _colliders = null;
    //[SerializeField] private Material _materialGreen = null;
    //[SerializeField] private Material _materialRed = null;

    //[SerializeField] private List<IPickerGhost> _pickeableConflictList = null;
    //[SerializeField] private List<Sentry> _sentryConflictList = null;
    //public Transform GetTransform()
    //{
    //    return transform;
    //}

    //public bool GetIsPlaceable()
    //{
    //    if (Base.Instance.Gold >= _price)
    //    {
    //        if (_pickeableConflictList == null)
    //        {
    //            return true;
    //        }
    //        if (_pickeableConflictList.Count <= 0)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    //public void PlaceGhost()
    //{
    //    Enable(true);
    //    foreach (var collider in _colliders)
    //    {
    //        collider.enabled = true;
    //    }
    //    Base.Instance.RemoveGold(_price);
    //}

    //public void EnableDragNDropVFX(bool enable)
    //{
    //    _dragNDropObject.SetActive(enable);
    //}

    //public void SetDragNDropVFXColorToGreen(bool setToGreen)
    //{
    //    if (setToGreen)
    //    {
    //        _dragNDropObject.GetComponent<MeshRenderer>().material = _materialGreen;

    //    }
    //    else
    //    {
    //        _dragNDropObject.GetComponent<MeshRenderer>().material = _materialRed;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (_pickeableConflictList == null)
    //    {
    //        _pickeableConflictList = new List<IPickerGhost>();
    //    }
    //    IPickerGhost otherPickable = other.GetComponentInParent<IPickerGhost>();
    //    Debug.Log(otherPickable != null);
    //    if (otherPickable != null)
    //    {
    //        _pickeableConflictList.Add(otherPickable);
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    IPickerGhost otherPickable = other.GetComponentInParent<IPickerGhost>();
    //    if (otherPickable != null)
    //    {
    //        _pickeableConflictList.Remove(otherPickable);
    //    }
    //}
}
