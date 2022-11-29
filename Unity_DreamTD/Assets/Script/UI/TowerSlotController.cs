//From Template modified by ALBERT Esteban
using UnityEngine;
using UnityEngine.InputSystem;

public enum State
{
    Available = 0,
    GhostVisible
}

public class TowerSlotController : MonoBehaviour
{
    [SerializeField]
    private TowerSlot[] _towerSlots = null;

    [System.NonSerialized]
    private State _state = State.Available;

    [System.NonSerialized]
    private TowerDescription _currentTowerDescription = null;

    public PlayerDrag GetPlayerDrag
    {
        get
        {
            return LevelReferences.Instance.PlayerDrag;
        }
    }

    private void OnEnable()
    {
        for (int i = 0, length = _towerSlots.Length; i < length; i++)
        {
            _towerSlots[i].OnTowerSlotClicked -= TowerSlotController_OnTowerSlotClicked;
            _towerSlots[i].OnTowerSlotClicked += TowerSlotController_OnTowerSlotClicked;
        }
    }

    private void OnDisable()
    {
        for (int i = 0, length = _towerSlots.Length; i < length; i++)
        {
            _towerSlots[i].OnTowerSlotClicked -= TowerSlotController_OnTowerSlotClicked;
        }
    }

    private void TowerSlotController_OnTowerSlotClicked(TowerSlot sender)
    {
        if (_state == State.Available)
        {
            _currentTowerDescription = sender.TowerDescription;
            ChangeState(State.GhostVisible);
        }
    }

    public void Selecting(InputAction.CallbackContext obj) //left click confirm
    {
        if (_state == State.GhostVisible && GetPlayerDrag.TrySetBuildingInAction())
        {
                ChangeState(State.Available);
        }
    }
    public void Cancelling(InputAction.CallbackContext obj) //right click cancel, not set up yet
    {
        if (_state == State.GhostVisible)
        {
            ChangeState(State.Available);
        }
    }

    public void ChangeState(State newState)
    {
        switch (newState)
        {
            case State.Available:
                {
                    GetPlayerDrag.DestroyDraggedItem();
                    GetPlayerDrag.ActivateDrag(false);
                    _currentTowerDescription = null;
                }
                break;
            case State.GhostVisible:
                {
                    GetPlayerDrag.ActivateWithGhost(_currentTowerDescription.Instantiate());
                }
                break;
            default:
                break;
        }
        _state = newState;
    }
}
