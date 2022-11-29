using UnityEngine;
using UnityEngine.InputSystem;

public class UsineSlotController : MonoBehaviour
{
    [SerializeField]
    private UsineSlot[] usineSlot = null;

    [System.NonSerialized]
    private State _state = State.Available;

    [System.NonSerialized]
    private UsineDescription usineDescription = null;

    public PlayerDrag GetPlayerDrag
    {
        get
        {
            return LevelReferences.Instance.PlayerDrag;
        }
    }

    private void OnEnable()
    {
        for (int i = 0, length = usineSlot.Length; i < length; i++)
        {
            usineSlot[i].OnUsineSlotClicked -= UsineSlotController_OnUsineSlotClicked;
            usineSlot[i].OnUsineSlotClicked += UsineSlotController_OnUsineSlotClicked;
        }
    }

    private void OnDisable()
    {
        for (int i = 0, length = usineSlot.Length; i < length; i++)
        {
            usineSlot[i].OnUsineSlotClicked -= UsineSlotController_OnUsineSlotClicked;
        }
    }

    private void UsineSlotController_OnUsineSlotClicked(UsineSlot sender)
    {
        if (_state == State.Available)
        {
            usineDescription = sender.UsineDescription;
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
                    usineDescription = null;
                }
                break;
            case State.GhostVisible:
                {
                    GetPlayerDrag.ActivateWithGhost(usineDescription.Instantiate());
                }
                break;
            default:
                break;
        }
        _state = newState;
    }
}
