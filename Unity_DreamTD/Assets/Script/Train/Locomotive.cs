using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

//Made By Melinon Remy
public class Locomotive : MonoBehaviour
{
    public List<Wagon> wagons;
    [Tooltip("Time between each transfer")]
    public float waitTime;

    //Move var
    [HideInInspector] public SplineFollower splineFollower;
    [HideInInspector] public float maxSpeed;
    private float timeToRestartRef = 0.0f;
    private float deceleration;
    [HideInInspector] public bool isBraking;
    //Trigger var
    private Transform closeTo;
    [HideInInspector] public List<GameObject> objectCollided;
    //Transfer var
    [HideInInspector] public List<Wagon> wagonsToCheck;
    private int wagonNumber;
    private bool isTransfering;
    //Object collided script
    [HideInInspector] public UsineBehaviour usineBehaviour;
    [HideInInspector] public TowerGetProjectile sentryGetProjectile;
    private TowersDatas towersDatas;

    //Prices
    private int _price = 0;
    public int Price => _price;

    private void Start()
    {
        //Set spline and speed
        splineFollower = GetComponent<SplineFollower>();
        maxSpeed = splineFollower.speed;
        foreach (Wagon wagon in wagons)
        {
            wagon.GetComponent<SplineFollower>().speed = maxSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Collide with train
        if (other.tag == "Train" && other.GetComponent<MeshRenderer>().enabled == true)
        {
            closeTo = other.transform;
        }
        //Collide with usine
        if (other.gameObject.GetComponent<UsineBehaviour>() != null)
        {
            objectCollided.Add(other.gameObject);
            OnTriggerUsine(other.gameObject, true);
        }
        //Collide with tower
        if (other.gameObject.GetComponent<TowerGetProjectile>() != null)
        {
            objectCollided.Add(other.gameObject);
            OnTriggerSentry(other.gameObject, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //remove train to start moving
        if (other.transform == closeTo)
        {
            closeTo = null;
        }
    }

    private void OnTriggerUsine(GameObject triggeredUsine, bool firstLoop)
    {
        if (triggeredUsine == objectCollided[0])
        {
            usineBehaviour = triggeredUsine.GetComponent<UsineBehaviour>();
            if (usineBehaviour.projectiles > 0 && !isTransfering)
            {
                foreach (Wagon wagon in wagons)
                {
                    if (wagon.type.typeSelected == usineBehaviour.type.typeSelected && wagon.projectiles < wagon.maxResources && wagon.GetComponent<MeshRenderer>().enabled == true)
                    {
                        wagonsToCheck.Add(wagon);
                    }
                }
                if (wagonsToCheck.Count > 0)
                {
                    //Stop train
                    isBraking = true;
                    CheckTransfertUsine(usineBehaviour, wagonsToCheck[0], firstLoop);
                }
                else
                {
                    FinishTransfer();
                }
            }
            else if (objectCollided.Count > 0)
            {
                FinishTransfer();
            }
        }
    }
    private void OnTriggerSentry(GameObject triggeredSentry, bool firstLoop)
    {
        if (triggeredSentry == objectCollided[0])
        {
            sentryGetProjectile = triggeredSentry.GetComponent<TowerGetProjectile>();
            if (sentryGetProjectile.projectiles < sentryGetProjectile.maxRessource && !isTransfering)
            {
                foreach (Wagon wagon in wagons)
                {
                    if (wagon.type.typeSelected == sentryGetProjectile.type.typeSelected && wagon.projectiles > 0 && wagon.GetComponent<MeshRenderer>().enabled == true)
                    {
                        wagonsToCheck.Add(wagon);
                    }
                }
                if (wagonsToCheck.Count > 0)
                {
                    //Stop train
                    isBraking = true;
                    CheckTransfertSentry(sentryGetProjectile, firstLoop);
                }
                else
                {
                    FinishTransfer();
                }
            }
            else if (objectCollided.Count > 0)
            {
                FinishTransfer();
            }
        }
    }

    private void StopTrain(int margin)
    {
        //Stop train when close enough
        if (deceleration < margin)
        {
            splineFollower.speed = 0;
            foreach (Wagon wagon in wagons)
            {
                wagon.GetComponent<SplineFollower>().speed = 0;
            }
            isBraking = false;
            timeToRestartRef = 0;
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
        //If a train is close
        if (closeTo != null)
        {
            StopTrain(6);
        }
        //Decelaration
        if (isBraking && objectCollided.Count > 0)
        {
            deceleration = (objectCollided[0].transform.position - transform.position).magnitude;
            StopTrain(5);
        }
        //Start moving
        else if (!isTransfering)
        {
            timeToRestartRef += 0.5f * Time.deltaTime;
            splineFollower.speed = Mathf.Lerp(0, maxSpeed, timeToRestartRef);
            foreach (Wagon wagon in wagons)
            {
                wagon.GetComponent<SplineFollower>().speed = Mathf.Lerp(0, maxSpeed, timeToRestartRef);
            }
        }
    }

    void CheckTransfertUsine(UsineBehaviour usine, Wagon wagon, bool firstLoop)
    {
        isTransfering = true;
        StartCoroutine(TransferingUsine(wagon, usine, waitTime, firstLoop));
    }
    void CheckTransfertSentry(TowerGetProjectile sentry, bool firstLoop)
    {
        isTransfering = true;
        towersDatas = sentry.transform.parent.GetComponent<TowerManager>().TowersData;
        StartCoroutine(TransferingSentry(sentry, waitTime, firstLoop));
    }

    private IEnumerator TransferingUsine(Wagon wagon, UsineBehaviour usine, float waitFor, bool firstLoop)
    {
        if(firstLoop)
        {
            yield return new WaitForSeconds(1);
        }
        if (usine.projectiles > 0 && wagon.projectiles < wagon.maxResources && wagon.type.typeSelected == usine.type.typeSelected)
        {
            wagon.projectiles++;
            usine.projectiles--;
            yield return new WaitForSeconds(waitFor);
            StartCoroutine(TransferingUsine(wagon, usine, waitFor, false));
        }
        else if(wagonNumber + 1 < wagonsToCheck.Count && wagonsToCheck[wagonNumber + 1].projectiles < wagonsToCheck[wagonNumber + 1].maxResources && usine.projectiles > 0 && wagonsToCheck[wagonNumber + 1].type.typeSelected == usine.type.typeSelected)
        {
            wagonNumber++;
            StartCoroutine(TransferingUsine(wagonsToCheck[wagonNumber], usine, waitFor, false));
        }
        else
        {
            FinishTransfer();
        }
    }
    private IEnumerator TransferingSentry(TowerGetProjectile sentry, float waitFor, bool firstLoop)
    {
        if (firstLoop)
        {
            yield return new WaitForSeconds(1);
        }
        if (wagonsToCheck[wagonNumber].projectiles > 0 && wagonsToCheck[wagonNumber].type.typeSelected == sentry.type.typeSelected && towersDatas.Projectiles[0].ProjectileAmmount < towersDatas.MaxProjectilesAmmount)
        {
            sentry.projectiles++;
            sentry.transform.parent.GetComponent<TowerManager>().TowersData.AddProjAmmount(0 , 1);
            wagonsToCheck[wagonNumber].projectiles--;
            yield return new WaitForSeconds(waitFor);
            StartCoroutine(TransferingSentry(sentry, waitFor, false));
        }
        else if (wagonNumber + 1 < wagonsToCheck.Count && wagonsToCheck[wagonNumber + 1].projectiles > 0 && wagonsToCheck[wagonNumber + 1].type.typeSelected == sentry.type.typeSelected && towersDatas.Projectiles[0].ProjectileAmmount < towersDatas.MaxProjectilesAmmount)
        {
            wagonNumber++;
            StartCoroutine(TransferingSentry(sentry, waitFor, false));
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
        //Check if there's other object to transfer with
        if (objectCollided.Count > 0)
        {
            if (objectCollided[0].GetComponent<UsineBehaviour>() != null)
            {
                OnTriggerUsine(objectCollided[0], false);
            }
            else if (objectCollided[0].GetComponent<TowerGetProjectile>() != null)
            {
                OnTriggerSentry(objectCollided[0], false);
            }
        }
        else
        {
            objectCollided.Clear();
            //Start moving
            splineFollower.speed = maxSpeed;
            foreach (Wagon wagons in wagons)
            {
                wagons.GetComponent<SplineFollower>().speed = maxSpeed;
            }
        }
    }
}
