using UnityEngine;

//Made by Alexandre Dorian
public class MainMenu_RailsAnimation : MonoBehaviour
{
    [SerializeField]
    private Vector3 _forwardVector;

    [SerializeField]
    private float _speed;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += _forwardVector * _speed * Time.deltaTime; 
    }
}
