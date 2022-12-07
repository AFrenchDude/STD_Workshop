//By ALBERT Esteban
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1.0f;
    [Range(0, 1)][SerializeField] private float _rotationScale_X = 0.0f;
    [Range(0, 1)][SerializeField] private float _rotationScale_Y = 0.0f;
    [Range(0, 1)][SerializeField] private float _rotationScale_Z = 0.0f;

    private void Update()
    {
        float rotationX = 10.0f * _rotationScale_X;
        float rotationY = 10.0f * _rotationScale_Y;
        float rotationZ = 10.0f * _rotationScale_Z;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(rotationX, rotationY, rotationZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}
