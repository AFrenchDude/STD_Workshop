using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

//Made By Melinon Remy, modified by ALBERT Esteban to update stats via S.O and UpdateWagonLocationOnSpline
public class Locomotive : MonoBehaviour
{
    [SerializeField] private TrainStats _trainStats = null;
    private int _currentWagonCountLevel = 1;
    private int _currentSpeedLevel = 1;
    private int _currentStorageLevel = 1;

    public List<Wagon> wagons;

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

    public TrainStats TrainStats => _trainStats;
    public int CurrentWagonCountLevel => _currentWagonCountLevel;
    public int CurrentSpeedLevel => _currentSpeedLevel;
    public int CurrentStorageLevel => _currentStorageLevel;

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

    private void Awake()
    {
        //Set spline and speed
        splineFollower = GetComponent<SplineFollower>();
        SetIsParked(false);

        LevelReferences.Instance.LocomotiveManager.AddLocomotiveToList(this);
    }
    private void Start()
    {
        UpdateEveryTrainStats();
    }

    private void OnTriggerEnter(Collider other)
    {

        //Collide with train
        if (other.tag == "Train" && other.gameObject.active == true)
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
            UnityEngine.Debug.Log(other.transform.root.gameObject.ToString() + " / " + (other.transform.root.GetComponent<TowerManager>() != null).ToString());


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
                    if (wagon.type.typeSelected == factoryData.ProjectileType.typeSelected && wagon.projectiles < wagon.MaxWagonStorage && wagon.gameObject.active == true)
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
            splineFollower.SetSpeed(0);
            isBraking = false;
            timeToRestartRef = 0;
        }
        else
        {
            splineFollower.SetSpeed(deceleration);
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
                splineFollower.SetSpeed(Mathf.Lerp(0, _trainStats.SpeedLevels[_currentSpeedLevel - 1], timeToRestartRef));
            }
        }
        else
        {
            StopTrain(5);
        }

        if (splineFollower.SplineSpeed > -10) //Let the SetupWagonPosition run on Start
        {
            UpdateWagonLocationOnSpline(_trainStats.WagonMargin);
        }
    }

    private void UpdateWagonLocationOnSpline(float margin) //Used so every wagons depends on the same splinefollower, preventing inacurracies in distances between wagons/locomotive
    {
        for (int i = 0; i < wagons.Count; i++)
        {
            float wagonMoveAmount = (splineFollower.moveAmount - (i + 1) * margin) % splineFollower.maxMoveAmount;
            wagons[i].transform.position = splineFollower.spline.GetPositionAtUnits(wagonMoveAmount);
            wagons[i].transform.forward = splineFollower.spline.GetForwardAtUnits(wagonMoveAmount);
        }
    }

    void CheckTransfertUsine(UsineBehaviour usine, Wagon wagon, bool firstLoop)
    {
        isTransfering = true;
        StartCoroutine(TransferingUsine(wagon, usine, usine.getFactoryData.Ammount, _trainStats.WaitTime, firstLoop));
    }
    void CheckTransfertSentry(List<Projectile> projectile, List<Wagon> wagon, bool firstLoop)
    {
        isTransfering = true;
        StartCoroutine(TransferingSentry(projectile, wagon, _trainStats.WaitTime, firstLoop));
    }

    private IEnumerator TransferingUsine(Wagon wagon, UsineBehaviour usine, int numberToGet, float waitFor, bool firstLoop)
    {
        if (firstLoop)
        {
            yield return new WaitForSeconds(1);
        }
        if (numberToGet > 0 && wagon.projectiles < wagon.MaxWagonStorage && wagon.type.typeSelected == usine.getFactoryData.ProjectileType.typeSelected)
        {
            numberToGet--;
            wagon.projectiles++;
            usine.getFactoryData.RemoveProjectile(1);
            LevelReferences.Instance.ScoreManager.AddScore(_trainStats.ScoreOnTransfer);
            yield return new WaitForSeconds(waitFor);
            StartCoroutine(TransferingUsine(wagon, usine, numberToGet, waitFor, false));
        }
        else if (wagonNumber + 1 < wagonsToCheck.Count && wagonsToCheck[wagonNumber + 1].projectiles < wagonsToCheck[wagonNumber + 1].MaxWagonStorage && numberToGet > 0 && wagonsToCheck[wagonNumber + 1].type.typeSelected == usine.getFactoryData.ProjectileType.typeSelected)
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
                LevelReferences.Instance.ScoreManager.AddScore(_trainStats.ScoreOnTransfer);
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
        if (objectCollided.Count > 0)
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
                splineFollower.SetSpeed(_trainStats.SpeedLevels[_currentSpeedLevel - 1]);
            }
        }

    }

    public void StartTrain()
    {
        //Start moving
        UpdateSpeed();
    }

    public void UpdateEveryTrainStats()
    {
        UpdateSpeed();
        UpdateWagonCount();
        UpdateWagonMaxStorage();
    }

    #region WagonCountUpgrades
    public void SetWagonCountLevel(int newWagonCountLevel)
    {
        _currentWagonCountLevel = newWagonCountLevel;
    }
    public void UpgradeWagonCountLevel()
    {
        if (_currentWagonCountLevel < _trainStats.MaxWagonCount)
        {
            _currentWagonCountLevel++;
        }
        UpdateWagonCount();
        LevelReferences.Instance.ScoreManager.AddScore(_trainStats.ScoreOnUpgrade);
    }
    public void UpdateWagonCount()
    {
        for (int i = 0; i < _currentWagonCountLevel; i++)
        {
            if (!wagons[i].hasTriggered)
            {
                wagons[i].gameObject.SetActive(true);
                wagons[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
        for (int i = _currentWagonCountLevel; i < wagons.Count - 1; i++)
        {
            if (wagons[i].hasTriggered)
            {
                wagons[i].gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region SpeedUpgrades
    public void SetSpeedLevel(int newSpeedLevel)
    {
        _currentSpeedLevel = newSpeedLevel;
        UpdateSpeed();
    }
    public void UpgradeSpeedLevel()
    {
        if (_currentSpeedLevel < _trainStats.SpeedLevels.Count)
        {
            _currentSpeedLevel++;
        }
        UpdateSpeed();
    }
    public void UpdateSpeed()
    {
            splineFollower.SetSpeed(_trainStats.SpeedLevels[_currentSpeedLevel - 1]);
    }
    #endregion

    #region StorageUpgrades
    public void SetStorageLevel(int newStorageLevel)
    {
        _currentStorageLevel = newStorageLevel;
        UpdateWagonMaxStorage();
    }
    public void UpgradeStorageLevel()
    {
        if (_currentStorageLevel < _trainStats.WagonMaxStorageLevels.Count)
        {
            _currentStorageLevel++;
        }
        UpdateWagonMaxStorage();
    }
    public void UpdateWagonMaxStorage()
    {
        foreach (Wagon wagons in wagons)
        {
            wagons.SetMaxStorage(_trainStats.WagonMaxStorageLevels[_currentStorageLevel - 1]);
        }
    }
    #endregion

}
