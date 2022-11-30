//By ALBERT Esteban
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GumArea : MonoBehaviour
{
    GumAreaData _gumAreaData = null;

    private List<PathFollower> _entityInArea;

    public void SetGumAreaData(GumAreaData gumAreaData)
    {
        _gumAreaData = gumAreaData;
    }
    private void Start()
    {
        Destroy(gameObject, _gumAreaData.GumLifeTime);
        _entityInArea = new List<PathFollower>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PathFollower otherPathFollower = other.GetComponentInParent<PathFollower>();
        if (otherPathFollower != null)
        {
            if (_entityInArea.Contains(otherPathFollower) == false)
            {
                _entityInArea.Add(otherPathFollower);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PathFollower otherPathFollower = other.GetComponentInParent<PathFollower>();
        if (otherPathFollower != null)
        {
            _entityInArea.Remove(otherPathFollower);
        }
    }
    private void Update()
    {
        foreach (var entity in _entityInArea)
        {
            Status_Slow entityStatusSlow = entity.GetComponentInParent<Status_Slow>();
            if (entityStatusSlow != null)
            {
                entityStatusSlow.ResetTimer();
            }
            else
            {
                Status_Slow currententityStatusSlow = entity.AddComponent<Status_Slow>();
                currententityStatusSlow.SetSlowDuration(_gumAreaData.SlowDuration);
                currententityStatusSlow.SetSlowStrength(_gumAreaData.SlowStrength);
            }
        }
    }

}
