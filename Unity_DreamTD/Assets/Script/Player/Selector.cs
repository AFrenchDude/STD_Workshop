using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

//Made by Melinon Remy
public class Selector : MonoBehaviour
{
    public GameObject towerHUD;
    public GameObject usineHUD;
    public GameObject HUDUpgrade;
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
                    openHUDref.OnDeselect();
                    openHUDref = null;
                }
                if (hit.transform.gameObject.GetComponent<HUDwhenSelect>() != null)
                {
                    //unplug before plug
                    HUDUpgrade.GetComponent<UpgradeType>().ResetUpgrade();
                    HUDUpgrade.GetComponent<UpgradeType>().SetType(hit.transform.gameObject);

                    HUDUpgrade.GetComponent<FollowOnScreen>().SetTarget(hit.transform);
                    HUDUpgrade.SetActive(true);

                    openHUDref = hit.transform.gameObject.GetComponent<HUDwhenSelect>();
                    openHUDref.OnSelect();
                }
            }
            else if (openHUDref != null && !isMouseOnUI)
            {
                HUDUpgrade.GetComponent<UpgradeType>().ResetUpgrade();
                HUDUpgrade.SetActive(false);

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
