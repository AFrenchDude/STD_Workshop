using UnityEngine;
using UnityEngine.InputSystem;

//Made by Remy Melinon
public class Movement : MonoBehaviour
{
    //player's orientation
    private Vector2 inputValue;
    private Vector3 inputValue3D;

    [Header("Player")]
    [SerializeField] private float speed;

    public void Move(InputAction.CallbackContext obj)
    {
        //when button is pressed
        if (obj.phase == InputActionPhase.Performed)
        {
            inputValue = obj.ReadValue<Vector2>();
            inputValue3D = new Vector3(inputValue.x, 0, inputValue.y);
        }
        //when button is released
        else if(obj.phase == InputActionPhase.Canceled)
        {
            inputValue3D = new Vector3(0,0,0);
        }
    }

    public void Update()
    {
        //Move
        transform.position += inputValue3D * speed * Time.deltaTime;
    }
}
