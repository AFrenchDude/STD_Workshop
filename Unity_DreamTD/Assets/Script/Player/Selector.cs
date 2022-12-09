using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

//Made by Melinon Remy, modified by ALBERT Esteban
public class Selector : MonoBehaviour
{
    public GameObject towerHUD;
    public GameObject usineHUD;
    [SerializeField] LayerMask interactibleLayer;
    private HUDwhenSelect openHUDref;
    private bool isMouseOnUI;

    public bool IsMouseOnUI => isMouseOnUI;
    public void Select(InputAction.CallbackContext obj)
    {

        //when button is pressed
        if (obj.phase == InputActionPhase.Canceled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, interactibleLayer) && !isMouseOnUI)
            {
                if (openHUDref != null)
                {
                    if (openHUDref == hit.transform.gameObject.GetComponent<HUDwhenSelect>()) //Prevent closing hud if trying to open the same as before
                    {
                        return;
                    }
                    openHUDref.OnDeselect();
                    openHUDref = null;
                }
                if (hit.transform.gameObject.GetComponent<HUDwhenSelect>() != null)
                {
                    openHUDref = hit.transform.gameObject.GetComponent<HUDwhenSelect>();
                    openHUDref.OnSelect();
                }
            }
            else if (openHUDref != null && !isMouseOnUI)
            {
                openHUDref.OnDeselect();
                openHUDref = null;
            }
        }
    }

    //Check if mouse is on UI
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject(PointerInputModule.kMouseLeftId))
        {
            isMouseOnUI = true;
        }
        else
        {
            isMouseOnUI = false;
        }
    }
}
