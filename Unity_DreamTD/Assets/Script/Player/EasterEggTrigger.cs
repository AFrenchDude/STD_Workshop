//By ALBERT Esteban
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EasterEggTrigger : MonoBehaviour
{
    [SerializeField] private List<int> _valuePerStep = new List<int>();
    [SerializeField] private GameObject _hiddenButton = null;

    private List<int> _valueHistory = new List<int>();
    private bool _egFlag = false;

    public void SelectInput(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            AddInputToHistory(1);
        }
    }
    public void CancelInput(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            AddInputToHistory(2);
        }
    }

    private void AddInputToHistory(int inputValue)
    {
        _valueHistory.Add(inputValue);
        if (_valueHistory.Count > _valuePerStep.Count)
        {
            _valueHistory.RemoveAt(0);
        }
        CheckValueHistory();
    }

    private void CheckValueHistory()
    {
        int currentHistoryTotal = 0;
        for (int i = 0; i < _valueHistory.Count; i++)
        {
            currentHistoryTotal += _valueHistory[i];
            if (currentHistoryTotal != _valuePerStep[i]) //Prevent other combinations with same total values from working
            {
                return;
            }
        }
        if (currentHistoryTotal == _valuePerStep[_valuePerStep.Count - 1])
        {
            if (_egFlag == false)
            {
                EnableEasterEgg();
            }
        }
    }

    private void EnableEasterEgg()
    {
        _hiddenButton.SetActive(true);
    }
}
