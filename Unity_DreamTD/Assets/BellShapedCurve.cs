using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BellShapedCurve : MonoBehaviour
{
    private Vector2 _startedPosisiton;

    private Transform _targetPosition;

    [SerializeField]
    private SphereCollider _radiusCollider;

    [SerializeField]
    private AnimationCurve _bellCurveShape;

    [SerializeField]
    private float _height;

    public UnityEvent DamageDone;


    public void SetUpCurve(Transform target)
    {
        _radiusCollider.enabled = false;
        transform.parent = target;
        _startedPosisiton = new Vector2(transform.localPosition.x, transform.localPosition.z);
    }

    public void Update()
    {
        Vector2 current_pos = new Vector2(transform.localPosition.x, transform.localPosition.z);

        float lerpValue = current_pos.magnitude / _startedPosisiton.magnitude;

        transform.localPosition = new Vector3(transform.localPosition.x, _bellCurveShape.Evaluate(lerpValue) * _height, transform.localPosition.z);

        if (transform.localPosition.magnitude <= 0.5f)
        {
            int Damage = GetComponent<Damager>().getDamage;
            transform.root.GetComponent<Damageable>().TakeDamage(Damage, out int health);
            DamageDone.Invoke();

            Destroy(gameObject);
        }
    }
}