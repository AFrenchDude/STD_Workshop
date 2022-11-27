using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//Made By Melinon Remy
public class Locomotive : MonoBehaviour
{
    public List<Wagon> wagons;
    public List<Wagon> wagonsToCheck;
    public float waitTime;

    [HideInInspector] public SplineFollower splineFollower;
    [HideInInspector] public float maxSpeed;
    private float deceleration;
    private Transform closeTo;
    [HideInInspector] public bool isBraking;
    [HideInInspector] public int index = 0;
    [HideInInspector] public List<GameObject> objectCollided;
    private int wagonNumber;
    private bool isTransfering;
    private float t = 0.0f;

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
        if (other.tag == "Train" && other.GetComponent<MeshRenderer>().enabled == true)
        {
            closeTo = other.transform;
        }
        if (other.gameObject.GetComponent<UsineBehaviour>() != null)
        {
            objectCollided.Add(other.gameObject);
            OnTriggerUsine(other.gameObject);
        }
        if (other.gameObject.GetComponent<SentryGetProjectile>() != null)
        {
            objectCollided.Add(other.gameObject);
            OnTriggerSentry(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == closeTo)
        {
            closeTo = null;
        }
    }

    private void OnTriggerUsine(GameObject triggeredUsine)
    {
        usineBehaviour = triggeredUsine.GetComponent<UsineBehaviour>();
        if (usineBehaviour.projectiles.Count > 0 && !isTransfering)
        {
            foreach (Wagon wagon in wagons)
            {
                if (wagon.type.typeSelected == usineBehaviour.type.typeSelected && wagon.projectiles.Count != wagon.maxResources && wagon.isActiveAndEnabled)
                {
                    wagonsToCheck.Add(wagon);
                }
            }
            if (wagonsToCheck.Count > 0)
            {
                //Stop train
                isBraking = true;
                if (triggeredUsine == objectCollided[0])
                {
                    CheckTransfertUsine(usineBehaviour, wagonsToCheck[0]);
                }
            }
        }
    }
    private void OnTriggerSentry(GameObject triggeredSentry)
    {
        sentryGetProjectile = triggeredSentry.GetComponent<SentryGetProjectile>();
        if (sentryGetProjectile.projectiles.Count != sentryGetProjectile.maxRessource && !isTransfering)
        {
            foreach (Wagon wagon in wagons)
            {
                if (wagon.type.typeSelected == sentryGetProjectile.type.typeSelected)
                {
                    wagonsToCheck.Add(wagon);
                }
            }
            if (wagonsToCheck.Count > 0)
            {
                //Stop train
                isBraking = true;
                if (triggeredSentry == objectCollided[0])
                {
                    CheckTransfertSentry(sentryGetProjectile);
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
            t = 0;
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
        if (closeTo != null)
        {
            StopTrain(6);
        }
        if (isBraking && objectCollided.Count > 0)
        {
            deceleration = (objectCollided[0].transform.position - transform.position).magnitude;
            StopTrain(5);
        }
        else if (!isTransfering)
        {
            t += 0.5f * Time.deltaTime;
            splineFollower.speed = Mathf.Lerp(0, maxSpeed, t);
            foreach (Wagon wagon in wagons)
            {
                wagon.GetComponent<SplineFollower>().speed = Mathf.Lerp(0, maxSpeed, t);
            }
        }
    }

    void CheckTransfertUsine(UsineBehaviour usine, Wagon wagon)
    {
        isTransfering = true;
        StartCoroutine(TransferingUsine(wagon, usine, usine.projectiles.Count, waitTime));
    }
    void CheckTransfertSentry(SentryGetProjectile sentry)
    {
        isTransfering = true;
        StartCoroutine(TransferingSentry(sentry, waitTime));
    }

    private IEnumerator TransferingUsine(Wagon wagon, UsineBehaviour usine, int numberToGet, float waitFor)
    {
        if (numberToGet > 0 && wagon.projectiles.Count != wagon.maxResources && wagon.type.typeSelected == usine.type.typeSelected)
        {
            numberToGet--;
            wagon.projectiles.Add(wagon.type.projectile);
            usine.projectiles.RemoveAt(usine.projectiles.Count - 1);
            yield return new WaitForSeconds(waitFor);
            StartCoroutine(TransferingUsine(wagon, usine, numberToGet, waitFor));
        }
        else if(wagonNumber + 1 < wagonsToCheck.Count && wagonsToCheck[wagonNumber + 1].projectiles.Count != wagonsToCheck[wagonNumber + 1].maxResources && numberToGet > 0 && wagonsToCheck[wagonNumber + 1].type.typeSelected == usine.type.typeSelected)
        {
            wagonNumber++;
            StartCoroutine(TransferingUsine(wagonsToCheck[wagonNumber], usine, numberToGet, waitFor));
        }
        else
        {
            FinishTransfer();
        }
    }
    private IEnumerator TransferingSentry(SentryGetProjectile sentry, float waitFor)
    {
        if (wagonsToCheck[wagonNumber].projectiles.Count > 0 && wagonsToCheck[wagonNumber].type.typeSelected == sentry.type.typeSelected)
        {
            sentry.projectiles.Add(sentry.type.projectile);
            wagonsToCheck[wagonNumber].projectiles.RemoveAt(wagonsToCheck[wagonNumber].projectiles.Count - 1);
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(TransferingSentry(sentry, waitFor));
        }
        else if (wagonNumber + 1 < wagonsToCheck.Count && wagonsToCheck[wagonNumber + 1].projectiles.Count > 0 && wagonsToCheck[wagonNumber + 1].type.typeSelected == sentry.type.typeSelected)
        {
            wagonNumber++;
            StartCoroutine(TransferingSentry(sentry, waitFor));
        }
        else
        {
            FinishTransfer();
        }
    }

    private void FinishTransfer()
    {
        wagonsToCheck.Clear();
        isTransfering = false;
        objectCollided.RemoveAt(0);
        if (objectCollided.Count > 0)
        {
            if (objectCollided[0].GetComponent<UsineBehaviour>() != null)
            {
                OnTriggerUsine(objectCollided[0]);
            }
            else if (objectCollided[0].GetComponent<SentryGetProjectile>() != null)
            {
                OnTriggerSentry(objectCollided[0]);
            }
        }
        else
        {
            splineFollower.speed = maxSpeed;
            foreach (Wagon wagons in wagons)
            {
                wagons.GetComponent<SplineFollower>().speed = maxSpeed;
            }
        }
    }
}
