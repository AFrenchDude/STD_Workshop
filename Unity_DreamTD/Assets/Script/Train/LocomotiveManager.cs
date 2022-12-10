//By ALBERT Esteban
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocomotiveManager : MonoBehaviour
{
    private List<Locomotive> _locomotiveList = new List<Locomotive>();
    public UnityEvent<List<Locomotive>> LocomotiveListUpdated;

    public List<Locomotive> GetLocomotives => _locomotiveList;
    public void AddLocomotiveToList(Locomotive newLocomotive)
    {
        _locomotiveList.Add(newLocomotive);
        LocomotiveListUpdated.Invoke(_locomotiveList);
    }

    public void ClearLocomotiveList()
    {
        _locomotiveList.Clear();
        LocomotiveListUpdated.Invoke(_locomotiveList);
    }

}
