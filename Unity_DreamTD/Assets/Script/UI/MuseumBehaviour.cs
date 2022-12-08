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
    //3D model
    [SerializeField] Transform model;
    private GameObject lastInstantiate = null;
    [SerializeField] RectTransform moverRef;
    private Vector2 lastmove = new Vector2(0, 0);

    //On open HUD
    public void OnMuseumOpen()
    {
        //Set unlocked enemies buttons
        foreach(Transform button in buttons)
        {
            if (button.GetComponent<MuseumButton>().NightmareBestiary.IsUnlocked)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
        //Set picked enemie in HUD as the first unlocked available
        foreach (Transform button in buttons)
        {
            if (button.gameObject.activeSelf)
            {
                ChangeEnemyDescription(button.GetComponent<MuseumButton>().NightmareBestiary);
                break;
            }
        }
    }

    //Pick new enemies
    public void ChangeEnemyDescription(NightmareBestiaryData nightmare)
    {
        //Destroy last enemy model if not null
        DestroyModel();
        //Create new enemy model
        var instantiatedNightmare = Instantiate(nightmare.EnemyModel, model.position, Quaternion.identity);
        //Set model settings
        instantiatedNightmare.transform.localScale *= 6;
        instantiatedNightmare.transform.eulerAngles = new Vector3(0, 180, 0);
        //Set actual enemy as the last saved
        lastInstantiate = instantiatedNightmare;

        //Set text of enemy name and description
        nameText.SetText("Name: " + nightmare.Name);
        descriptionText.SetText("Description: " + nightmare.Description + "<br>" + "Type: " + nightmare.NightmareData.nighmareType + "<br>" + "Role: " + nightmare.NightmareData.nightmareFunction +
            "<br><br>" + "Speed: " + nightmare.NightmareData.speed + "<br>" + "Gold reward: " + nightmare.NightmareData.rewardGold + "<br>" + 
            "MaxLife: " + nightmare.NightmareData.maxLife + "<br>" + "Killcount: " + nightmare.NightmareData.KillCount);
    }

    //Destroy current enemy 3D model
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
