using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    [SerializeField] LayerMask interactibleLayer;
    [SerializeField] GameObject hud;

    public void Select(InputAction.CallbackContext obj)
    {
        //when button is pressed
        if (obj.phase == InputActionPhase.Started)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, interactibleLayer))
            {/*
                if(hud.activeSelf == false)
                {
                    hud.SetActive(true);
                }
                else
                {
                    hud.SetActive(false);
                }*/
            }
            else
            {
                //hud.SetActive(false);
            }
        }
    }
}
