//By ALBERT Esteban
using UnityEngine;

public class Status_Slow : MonoBehaviour
{
    [Range(0, 1)][SerializeField] private float _slowDuration = 2.0f;
    [SerializeField] private float _slowStrength = 0.5f;
    private PathFollower _pathFollower = null;
    private float _slowStartTime = 0.0f;
    private float _originalSpeed = 0.0f;

    private void OnEnable()
    {
        _pathFollower = GetComponentInParent<PathFollower>();
        _originalSpeed = _pathFollower.OGSpeed;
        _slowStartTime = Time.time;
        UpdateSlowStatus();
    }
    private void Update()
    {
        if (Time.time >= _slowStartTime + _slowDuration)
        {
            _pathFollower.SetSpeed(_originalSpeed);
            Destroy(this);
        }
    }

    public void ResetTimer()
    {
        _slowStartTime = Time.time;
    }

    public void SetSlowDuration(float duration)
    {
        _slowDuration = duration;
    }

    public void SetSlowStrength(float strength)
    {
        _slowStrength = strength;
        UpdateSlowStatus();
    }

    public void UpdateSlowStatus()
    {
        _pathFollower.SetSpeed(_originalSpeed * (1 - _slowStrength));
    }
}
