//By ALBERT Esteban
using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] float _rotationScale_X = 0.0f;
    [Range(0.0f, 1.0f)][SerializeField] float _rotationScale_Y = 0.0f;
    [Range(0.0f, 1.0f)][SerializeField] float _rotationScale_Z = 0.0f;

    private void Update()
    {
        Quaternion newRotation = transform.rotation * Quaternion.Euler(10.0f * _rotationScale_X, 10.0f * _rotationScale_Y, 10.0f * _rotationScale_Z);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
    }
}
