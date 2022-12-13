using TMPro;
using UnityEngine;

//Made by Melinon Remy
public class MuseumBehaviour : MonoBehaviour
{
    //Texts
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    //List of all buttons
    [SerializeField] Transform buttons;
    [SerializeField] GameObject levelButtons;
    //3D model position ref
    [SerializeField] Transform modelPositionRef;
    private GameObject lastInstantiate = null;
    private GameObject lastButton;
    private TowerUpgradeData currentTowerUpgrade;
    private FactoryUpgradeData currentFactoryUpgrade;
    [SerializeField] RectTransform moverRef;
    private Vector2 lastmove = new Vector2(0, 0);

    //On open HUD
    public void OnMuseumOpen()
    {
        //Set unlocked enemies buttons
        foreach(Transform button in buttons)
        {
            if (button.GetComponent<MuseumButton>().NightmareBestiary != null)
            {
                if (!button.GetComponent<MuseumButton>().NightmareBestiary.IsUnlocked)
                {
                    button.gameObject.SetActive(false);
                }
            }
            else if (button.GetComponent<MuseumButton>().FactoryData != null)
            {
                if (!button.GetComponent<MuseumButton>().FactoryData.IsUnlocked)
                {
                    button.gameObject.SetActive(false);
                }
            }
            else if (button.GetComponent<MuseumButton>().TowersDatas != null)
            {
                if (!button.GetComponent<MuseumButton>().TowersDatas.IsUnlocked)
                {
                    button.gameObject.SetActive(false);
                }
            }
        }
        //Set picked enemie in HUD as the first unlocked available
        foreach (Transform button in buttons)
        {
            if (button.gameObject.activeSelf)
            {
                if (button.GetComponent<MuseumButton>().NightmareBestiary != null)
                {
                    ChangeEnemyDescription(button.GetComponent<MuseumButton>().NightmareBestiary);
                    break;
                }
                else if (button.GetComponent<MuseumButton>().FactoryData != null)
                {
                    lastButton = button.gameObject;
                    ChangeUsineDescription(button.GetComponent<MuseumButton>().FactoryData);
                    levelButtons.SetActive(true);
                    break;
                }
                else if (button.GetComponent<MuseumButton>().TowersDatas != null)
                {
                    lastButton = button.gameObject;
                    ChangeTowerDescription(button.GetComponent<MuseumButton>().TowersDatas);
                    levelButtons.SetActive(true);
                    break;
                }
            }
        }
    }

    //Change level of tower or usine
    public void ChangeLevel(int upgradeRef)
    {
        if (lastButton.GetComponent<MuseumButton>().TowersDatas != null)
        {
            currentTowerUpgrade = lastButton.GetComponent<MuseumButton>().TowersUpgradeDatas[upgradeRef];
            ChangeTowerDescription(lastButton.GetComponent<MuseumButton>().TowersDatas);
            currentFactoryUpgrade = null;
        }
        else if (lastButton.GetComponent<MuseumButton>().FactoryData != null)
        {
            currentFactoryUpgrade = lastButton.GetComponent<MuseumButton>().FactoryUpgradeData[upgradeRef];
            ChangeUsineDescription(lastButton.GetComponent<MuseumButton>().FactoryData);
            currentTowerUpgrade = null;
        }
    }

    //Save object picked
    public void SetButton(GameObject button)
    {
        lastButton = button;
        if (button.GetComponent<MuseumButton>().TowersDatas != null)
        {
            currentTowerUpgrade = button.GetComponent<MuseumButton>().TowersUpgradeDatas[0];
            ChangeTowerDescription(button.GetComponent<MuseumButton>().TowersDatas);
            currentFactoryUpgrade = null;
        }
        else if (button.GetComponent<MuseumButton>().FactoryData != null)
        {
            currentFactoryUpgrade = button.GetComponent<MuseumButton>().FactoryUpgradeData[0];
            ChangeUsineDescription(button.GetComponent<MuseumButton>().FactoryData);
            currentTowerUpgrade = null;
        }
    }

    //Pick new enemies
    public void ChangeEnemyDescription(NightmareBestiaryData nightmare)
    {
        //Set text of enemy _name and description
        nameText.SetText("Name: " + nightmare.Name);
        descriptionText.SetText("Description: " + nightmare.Description + "<br>" +
            "Type: " + nightmare.NightmareData.nighmareType + "<br>" +
            "Role: " + nightmare.NightmareData.nightmareFunction + "<br>" +
            "Weakness: " + nightmare.NightmareData.weakness + "<br><br>" +
            "Speed: " + nightmare.NightmareData.speed + "<br>" + 
            "Gold reward: " + nightmare.NightmareData.rewardGold + "<br>" + 
            "Max life: " + nightmare.NightmareData.maxLife + "<br><br>" + 
            "Killcount: " + nightmare.NightmareData.KillCount + " killed");
        Set3Dmodel(nightmare.EnemyModel, new Vector3(30, -220, -15), 10);
    }

    //Pick new tower
    public void ChangeTowerDescription(TowersDatas towersDatas)
    {
        if (currentTowerUpgrade == null)
        {
            currentTowerUpgrade = towersDatas.UpgradeDatas;
        }
        //Set text of enemy _name and description
        nameText.SetText("Name: " + towersDatas.Name + " (" + currentTowerUpgrade.UpgradeName + ")");
        descriptionText.SetText("Description: " + towersDatas.Description + "<br>" +
            "Type: " + towersDatas.Type + "<br><br>" +
            "Damage: " + currentTowerUpgrade.UpgradeDamage+ "<br>" +
            "Fire cooldown: " + currentTowerUpgrade.UpgradeFireRate + "s" + "<br>" +
            "Max projectiles: " + currentTowerUpgrade.UpgradeMaxProjectiles + "<br>" +
            "Range: " + currentTowerUpgrade.UpgradeRange);
        Set3Dmodel(currentTowerUpgrade.UpgradePrefab, new Vector3(30, 110, -35), 6);
    }

    //Pick new usine
    public void ChangeUsineDescription(FactoryDatas factoryUpgradeData)
    {
        if (currentFactoryUpgrade == null)
        {
            currentFactoryUpgrade = factoryUpgradeData.CurrentUpgrade;
        }
        //Set text of enemy _name and description
        nameText.SetText("Name: " + factoryUpgradeData.Name + " (" + currentFactoryUpgrade.UpgradeName + ")");
        descriptionText.SetText("Description: " + factoryUpgradeData.UpgradeDescription + "<br>" +
            "Type: " + factoryUpgradeData.Type + "<br><br>" +
            "Production rate: " + currentFactoryUpgrade.UpgradeCooldown + "s" + "<br>" +
            "Price: " + currentFactoryUpgrade.UpgradePrice + "<br>" +
            "Max resources: " + currentFactoryUpgrade.UpgradeMaxResource);
        Set3Dmodel(currentFactoryUpgrade.UpgradePrefab, new Vector3(-15, -70, 15), 6);
    }

    //Model 3D
    void Set3Dmodel(GameObject model, Vector3 rotation, int scale)
    {
        //Destroy last enemy modelPositionRef if not null
        DestroyModel();
        //Create new enemy modelPositionRef
        var instantiatedNightmare = Instantiate(model, modelPositionRef.position, Quaternion.identity);
        //Set modelPositionRef settings
        instantiatedNightmare.transform.localScale *= scale;
        instantiatedNightmare.transform.eulerAngles = rotation;
        //Play animation
        if (instantiatedNightmare.GetComponent<Animator>() != null)
        {
            instantiatedNightmare.GetComponent<Animator>().SetBool("Activated", true);
        }
        //Set actual enemy as the last saved
        lastInstantiate = instantiatedNightmare;
    }
    //Destroy current enemy 3D modelPositionRef
    public void DestroyModel()
    {
        if (lastInstantiate != null)
        {
            Destroy(lastInstantiate);
        }
    }

    //Set enemy 3D model rotation
    private void Start()
    {
        lastmove = new Vector2(moverRef.transform.position.x, moverRef.transform.position.y);
    }
    public void Update()
    {
        if (lastInstantiate != null)
        {
            //Move vertical rotation
            float moveX = 0;
            moveX = -(moverRef.transform.position.y - lastmove.y);
            Vector3 rotationY = new Vector3(moveX * -4, 0, 0);
            lastInstantiate.transform.Rotate(rotationY, Space.World);
            //Move horizontal rotation
            float moveY = 0;
            moveY = -(moverRef.transform.position.x - lastmove.x);
            Vector3 rotationX = new Vector3(0, moveY * 4, 0);
            lastInstantiate.transform.Rotate(rotationX);
            //Set actual position of mover as the last saved
            lastmove = new Vector2(moverRef.transform.position.x, moverRef.transform.position.y);
        }
    }
}
