using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool isBraking;
    //Trigger var
    private Transform closeTo;
    public List<GameObject> objectCollided = new List<GameObject>();
    //Transfer var
    [HideInInspector] public List<Wagon> wagonsToCheck;
    private int wagonNumber;
    private bool isTransfering;

    [SerializeField]
    private bool isParked;

    public void SetIsParked(bool enable)
    {
        isParked = enable;
    }

    //Object collided script
    [HideInInspector] public FactoryDatas factoryData;
    [HideInInspector] public TowersDatas towerDatas;

    //Prices
    private int _price = 0;
    public int Price => _price;

    private void Start()
    {
        //Set spline and speed
        splineFollower = GetComponent<SplineFollower>();
        maxSpeed = splineFollower.speed;
        SetIsParked(false);
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
            if (other.transform.root.gameObject != transform.root.gameObject)
            {
                closeTo = other.transform;
            }

        }
        //Collide with usine
        if (other.gameObject.GetComponent<UsineBehaviour>() != null)
        {
            objectCollided.Add(other.gameObject);
            OnTriggerUsine(other.gameObject, true);
        }
        //Collide with tower
        if (other.CompareTag("TowerTrain"))
        {
            Debug.Log(other.transform.root.gameObject.ToString() + " / " + (other.transform.root.GetComponent<TowerManager>() != null).ToString());


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
            factoryData = triggeredUsine.GetComponent<UsineBehaviour>().getFactoryData;
            if (factoryData.Ammount > 0 && !isTransfering)
            {
                foreach (Wagon wagon in wagons)
                {
                    if (wagon.type.typeSelected == factoryData.ProjectileType.typeSelected && wagon.projectiles < wagon.maxResources && wagon.GetComponent<MeshRenderer>().enabled == true)
                    {
                        wagonsToCheck.Add(wagon);
                    }
                }
                if (wagonsToCheck.Count > 0)
                {
                    //Stop train
                    isBraking = true;
                    CheckTransfertUsine(triggeredUsine.GetComponent<UsineBehaviour>(), wagonsToCheck[0], firstLoop);
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

            //Initialize lists
            List<Projectile> connectedProjectile = new List<Projectile>();
            List<Wagon> connectedWagon = new List<Wagon>();

            //Ref to tower datas
            towerDatas = triggeredSentry.transform.root.GetComponent<TowerManager>().TowersData;

            //Connect each wagon to his projectile reference
            foreach (Wagon wagon in wagons)
            {
                if (wagon.projectiles > 0)
                {
                    foreach (Projectile projectile in towerDatas.Projectiles)
                    {
                        if (wagon.type == projectile.ProjectileType & projectile.ProjectileAmmount < projectile.MaxProjectilesAmmount)
                        {
                            connectedProjectile.Add(projectile);
                            connectedWagon.Add(wagon);
                        }
                    }

                }
            }

            if (connectedProjectile.Count > 0)
            {

                //Stop train
                isBraking = true;

                CheckTransfertSentry(connectedProjectile, connectedWagon, firstLoop);

            }
            else
            {
                FinishTransfer();
            }



        }
    }

    public void StopTrain(int margin)
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
        if (!isParked)
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
        else
        {
            StopTrain(5);
        }
    }

    void CheckTransfertUsine(UsineBehaviour usine, Wagon wagon, bool firstLoop)
    {
        isTransfering = true;
        StartCoroutine(TransferingUsine(wagon, usine, usine.getFactoryData.Ammount, waitTime, firstLoop));
    }
    void CheckTransfertSentry(List<Projectile> projectile, List<Wagon> wagon, bool firstLoop)
    {
        isTransfering = true;
        StartCoroutine(TransferingSentry(projectile, wagon, waitTime, firstLoop));
    }

    private IEnumerator TransferingUsine(Wagon wagon, UsineBehaviour usine, int numberToGet, float waitFor, bool firstLoop)
    {
        if (firstLoop)
        {
            yield return new WaitForSeconds(1);
        }
        if (numberToGet > 0 && wagon.projectiles < wagon.maxResources && wagon.type.typeSelected == usine.getFactoryData.ProjectileType.typeSelected)
        {
            numberToGet--;
            wagon.projectiles++;
            usine.getFactoryData.RemoveProjectile(1);
            yield return new WaitForSeconds(waitFor);
            StartCoroutine(TransferingUsine(wagon, usine, numberToGet, waitFor, false));
        }
        else if (wagonNumber + 1 < wagonsToCheck.Count && wagonsToCheck[wagonNumber + 1].projectiles < wagonsToCheck[wagonNumber + 1].maxResources && numberToGet > 0 && wagonsToCheck[wagonNumber + 1].type.typeSelected == usine.getFactoryData.ProjectileType.typeSelected)
        {
            wagonNumber++;
            StartCoroutine(TransferingUsine(wagonsToCheck[wagonNumber], usine, numberToGet, waitFor, false));
        }
        else
        {
            FinishTransfer();
        }
    }
    private IEnumerator TransferingSentry(List<Projectile> projectile, List<Wagon> wagon, float waitFor, bool firstLoop)
    {
        bool passOneCondition = false;
        if (firstLoop)
        {
            yield return new WaitForSeconds(1);
        }
        for (int i = 0; i < wagon.Count; i++)
        {
            if (wagon[i].projectiles > 0 && wagon[i].type == projectile[i].ProjectileType && projectile[i].ProjectileAmmount < projectile[i].MaxProjectilesAmmount)
            {
                passOneCondition = true;
                projectile[i].ProjectileAmmount++;
                wagon[i].projectiles--;
            }
        }

        yield return new WaitForSeconds(waitFor);

        if (passOneCondition == false)
        {
            FinishTransfer();
        }
        else
        {
            StartCoroutine(TransferingSentry(projectile, wagon, waitFor, false));
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

    public void StartTrain()
    {
        //Start moving
        splineFollower.speed = maxSpeed;
        foreach (Wagon wagons in wagons)
        {
            wagons.GetComponent<SplineFollower>().speed = maxSpeed;
        }
    }
}
