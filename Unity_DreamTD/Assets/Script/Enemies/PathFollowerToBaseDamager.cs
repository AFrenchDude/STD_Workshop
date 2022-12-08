//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowerToBaseDamager : MonoBehaviour
{
    private void OnEnable()
    {
        PathFollower pathFollower = GetComponent<PathFollower>();
        if (pathFollower != null)
        {
            pathFollower.LastWaypointReached.RemoveListener(TryCallBaseDamager);
            pathFollower.LastWaypointReached.AddListener(TryCallBaseDamager);
        }
        else
        {
            throw new System.Exception("PathFollowerToBaseDamager on a GameObject without a PathFollower");
        }
    }

    private void OnDisable()
    {
        PathFollower pathFollower = GetComponent<PathFollower>();
        if (pathFollower != null)
        {
            pathFollower.LastWaypointReached.RemoveListener(TryCallBaseDamager);
        }
        else
        {
            throw new System.Exception("PathFollowerToBaseDamager on a GameObject without a PathFollower");
        }
    }

    private void TryCallBaseDamager(PathFollower pathFollower)
    {
        BaseDamager baseDamager = GetComponent<BaseDamager>();
        if (baseDamager != null)
        {
            baseDamager.DamageBase();
        }
        else
        {
            throw new System.Exception("PathFollowerToBaseDamager tried to call BaseDamager but it does not exist on this GameObject");
        }
    }
}
