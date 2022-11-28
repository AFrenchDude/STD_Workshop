using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

//Made by Melinon Remy
public class Selector : MonoBehaviour
{
    [SerializeField] private GameObject towerHUD;
    [SerializeField] LayerMask interactibleLayer;
    private HUDwhenSelect openHUDref;
    private bool isMouseOnUI;

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
                    openHUDref.OnClick(false);
                }
                if (hit.transform.gameObject.GetComponent<HUDwhenSelect>() != null)
                {
                    openHUDref = hit.transform.gameObject.GetComponent<HUDwhenSelect>();
                    if (hit.transform.gameObject.GetComponent<Tower>() != null)
                    {
                        openHUDref.hudRef = towerHUD;
                    }
                    openHUDref.OnClick(true);
                }
            }
            else if (openHUDref != null && !isMouseOnUI)
            {
                openHUDref.OnClick(false);
            }
        }
    }

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
