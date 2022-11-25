using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Made By Melinon Remy
public class Locomotive : MonoBehaviour
{
    public List<Wagon> wagons;
    public List<Wagon> wagonsToCheck;

    [HideInInspector] public SplineFollower splineFollower;
    [HideInInspector] public float maxSpeed;
    public float deceleration;
    private Transform closeTo;
    public bool isBraking;
    public float waitTime;
    [HideInInspector] public int index = 0;
    public List<GameObject> objectCollided;
    private int wagonNumber;

    [HideInInspector] public UsineBehaviour usineBehaviour;
    [HideInInspector] public SentryGetProjectile sentryGetProjectile;

    private void Start()
    {
        splineFollower = GetComponent<SplineFollower>();
        maxSpeed = splineFollower.speed;
        foreach (Wagon wagon in wagons)
        {
            wagon.GetComponent<SplineFollower>().speed = maxSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Train")
        {
            closeTo = other.transform;
        }
        if (other.gameObject.GetComponent<UsineBehaviour>() != null)
        {
            usineBehaviour = other.gameObject.GetComponent<UsineBehaviour>();
            if (usineBehaviour.projectiles.Count > 0)
            {
                objectCollided.Add(other.gameObject);
                foreach (Wagon wagon in wagons)
                {
                    if (wagon.type.typeSelected == usineBehaviour.type.typeSelected && wagon.projectiles.Count != wagon.maxResources)
                    {
                        //Stop train
                        isBraking = true;
                        wagonsToCheck.Add(wagon);
                    }
                }
                if (wagonsToCheck.Count > 0)
                {
                    CheckTransfertUsine(usineBehaviour, wagonsToCheck[0]);
                }
                else
                {
                    objectCollided.Remove(other.gameObject);
                }
            }
        }
        if (other.gameObject.GetComponent<SentryGetProjectile>() != null)
        {
            sentryGetProjectile = other.gameObject.GetComponent<SentryGetProjectile>();
            if (sentryGetProjectile.projectiles.Count != sentryGetProjectile.maxRessource)
            {
                objectCollided.Add(other.transform.parent.gameObject);
                foreach (Wagon wagon in wagons)
                {
                    if (wagon.type.typeSelected == sentryGetProjectile.type.typeSelected)
                    {
                        //Stop train
                        isBraking = true;
                        wagonsToCheck.Add(wagon);
                    }
                }
                if(wagonsToCheck.Count > 0)
                {
                    CheckTransfertSentry(sentryGetProjectile);
                }
                else
                {
                    objectCollided.Remove(other.gameObject);
                }
            }
        }
    }

    private void StopTrain(int margin)
    {
        if (deceleration < margin)
        {
            splineFollower.speed = 0;
            foreach (Wagon wagon in wagons)
            {
                wagon.GetComponent<SplineFollower>().speed = 0;
            }
            isBraking = false;
        }
        else
        {
            splineFollower.speed = deceleration;
            foreach (Wagon wagon in wagons)
            {
                wagon.GetComponent<SplineFollower>().speed = deceleration;
            }
        }
    }

    private void Update()
    {
        if (isBraking && objectCollided.Count > 0)
        {
            deceleration = (objectCollided[0].transform.position - transform.position).magnitude;
            StopTrain(4);
        }
        if (closeTo != null)
        {
            StopTrain(6);
        }
    }

    void CheckTransfertUsine(UsineBehaviour usine, Wagon wagon)
    {
        StartCoroutine(TransferingUsine(wagon, usine, usine.projectiles.Count, waitTime));
    }
    void CheckTransfertSentry(SentryGetProjectile sentry)
    {
        StartCoroutine(TransferingSentry(sentry, waitTime));
    }

    private IEnumerator TransferingUsine(Wagon wagon, UsineBehaviour usine, int numberToGet, float waitFor)
    {
        if (numberToGet > 0 && wagon.projectiles.Count != wagon.maxResources)
        {
            numberToGet--;
            wagon.projectiles.Add(wagon.type.projectile);
            usine.projectiles.RemoveAt(usine.projectiles.Count - 1);
            yield return new WaitForSeconds(waitFor);
            StartCoroutine(TransferingUsine(wagon, usine, numberToGet, waitFor));
        }
        else if(wagon.projectiles.Count == wagon.maxResources && numberToGet > 0 && wagonsToCheck[wagonNumber + 1] != null)
        {
            wagonNumber++;
            StartCoroutine(TransferingUsine(wagonsToCheck[wagonNumber], usine, numberToGet, waitFor));
        }
        else
        {
            objectCollided.RemoveAt(0);
            splineFollower.speed = maxSpeed;
            foreach (Wagon wagons in wagons)
            {
                wagons.GetComponent<SplineFollower>().speed = maxSpeed;
            }
        }
    }
    private IEnumerator TransferingSentry(SentryGetProjectile sentry, float waitFor)
    {
        if (wagonsToCheck[wagonNumber].projectiles.Count > 0)
        {
            sentry.projectiles.Add(sentry.type.projectile);
            wagonsToCheck[wagonNumber].projectiles.RemoveAt(wagonsToCheck[wagonNumber].projectiles.Count - 1);
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(TransferingSentry(sentry, waitFor));
        }
        else if (wagonsToCheck[wagonNumber + 1].projectiles.Count > 0 && wagonsToCheck[wagonNumber + 1] != null)
        {
            wagonNumber++;
            StartCoroutine(TransferingSentry(sentry, waitFor));
        }
        else
        {
            objectCollided.RemoveAt(0);
            splineFollower.speed = maxSpeed;
            foreach (Wagon wagons in wagons)
            {
                wagons.GetComponent<SplineFollower>().speed = maxSpeed;
            }
        }
    }
}
