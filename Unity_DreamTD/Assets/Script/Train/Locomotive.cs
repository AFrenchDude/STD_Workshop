using System.Collections.Generic;
using UnityEngine;

public class Locomotive : MonoBehaviour
{
    public List<Wagon> wagons;

    [HideInInspector] public SplineFollower splineFollower;
    [HideInInspector] public float maxSpeed;
    public float deceleration;
    [SerializeField] private float _acceleration = 5.0f;
    private Transform closeTo;
    public bool isTransferring;

    [HideInInspector] public UsineBehaviour usineBehaviour;
    [HideInInspector] public SentryGetProjectile sentryGetProjectile;

    private void Start()
    {
        splineFollower = GetComponent<SplineFollower>();
        maxSpeed = splineFollower.speed;
        SetWagonSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Train")
        {
            closeTo = other.transform;
        }
        for (var i = 0; i != wagons.Count; i++)
        {
            if (other.gameObject.GetComponent<UsineBehaviour>() != null)
            {
                usineBehaviour = other.gameObject.GetComponent<UsineBehaviour>();
                if (usineBehaviour.projectiles.Count > 0)
                {
                    if (wagons[i].type.typeSelected == usineBehaviour.type.typeSelected && wagons[i].projectiles.Count != wagons[i].maxResources)
                    {
                        isTransferring = true;
                        wagons[i].AddRessources(usineBehaviour.projectiles.Count, 0.2f);
                        usineBehaviour.TransferCoroutine(wagons[i].maxResources - wagons[i].projectiles.Count, wagons[i].maxResources, 0.2f, this);
                    }
                }
            }
            if (other.gameObject.GetComponent<SentryGetProjectile>() != null)
            {
                sentryGetProjectile = other.gameObject.GetComponent<SentryGetProjectile>();
                if (wagons[i].projectiles.Count > 0)
                {
                    if (wagons[i].type.typeSelected == sentryGetProjectile.type.typeSelected && sentryGetProjectile.projectiles.Count < sentryGetProjectile.maxRessource)
                    {
                        isTransferring = true;
                        wagons[i].GiveRessources(sentryGetProjectile.maxRessource, 0.2f, this);
                        sentryGetProjectile.TransferCoroutine(wagons[i].projectiles.Count, 0.2f, this);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isTransferring)
        {
            usineBehaviour = null;
            splineFollower.speed = maxSpeed;
            SetWagonSpeed();
        }
        if (!isTransferring)
        {
            sentryGetProjectile = null;
            splineFollower.speed = maxSpeed;
            SetWagonSpeed();
        }
        if (other.tag == "Train")
        {
            closeTo = null;
            splineFollower.speed = maxSpeed;
            if(splineFollower.speed == maxSpeed)
            {
                SetWagonSpeed();
            }
        }
    }

    private void Update()
    {
        if (isTransferring && sentryGetProjectile != null)
        {
            deceleration = (sentryGetProjectile.transform.position - transform.position).magnitude;
            if (deceleration < 8)
            {
                splineFollower.speed = 0;
                SetWagonSpeed();
            }
            else
            {
                splineFollower.speed = deceleration;
                SetWagonSpeed();
            }
        }
        if(isTransferring && usineBehaviour != null)
        {
            deceleration = (usineBehaviour.transform.position - transform.position).magnitude;
            if (deceleration < 8)
            {
                splineFollower.speed = 0;
                SetWagonSpeed();
            }
            else
            {
                splineFollower.speed = deceleration;
                SetWagonSpeed();
            }
        }
        if(closeTo != null)
        {
            deceleration = (closeTo.transform.position - transform.position).magnitude;
            if (deceleration < 6)
            {
                splineFollower.speed = 0;
                SetWagonSpeed();
            }
            else
            {
                splineFollower.speed = deceleration;
                SetWagonSpeed();
            }
        }
    }

    public void SetWagonSpeed()
    {
        for (var i = 0; i != wagons.Count; i++)
        {
            wagons[i].gameObject.GetComponent<SplineFollower>().speed = splineFollower.speed;
        }
    }
}
