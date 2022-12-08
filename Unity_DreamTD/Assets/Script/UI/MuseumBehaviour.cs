using TMPro;
using UnityEngine;

//Made by Melinon Remy
public class MuseumBehaviour : MonoBehaviour
{
    [SerializeField] Transform model;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] RectTransform moverRef;
    [SerializeField] Transform buttons;
    private Vector2 lastmove = new Vector2(0, 0);
    private GameObject lastInstantiate = null;

    public void OnMuseumOpen()
    {
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
    }

    public void ChangeEnemyDescription(NightmareBestiaryData nightmare)
    {
        DestroyModel();
        var instantiatedNightmare = Instantiate(nightmare.EnemyModel, model.position, Quaternion.identity);
        instantiatedNightmare.transform.localScale *= 8;
        instantiatedNightmare.transform.eulerAngles = new Vector3(0, 180, 0);
        lastInstantiate = instantiatedNightmare;

        nameText.SetText("Name: " + nightmare.Name);
        descriptionText.SetText("Description: " + nightmare.Description + "<br>" + "Type: " + nightmare.NightmareData.nighmareType + "<br>" + "Role: " + nightmare.NightmareData.nightmareFunction +
            "<br><br>" + "Speed: " + nightmare.NightmareData.speed + "<br>" + "Gold reward: " + nightmare.NightmareData.rewardGold + "<br>" + 
            "MaxLife: " + nightmare.NightmareData.maxLife + "<br>" + "Killcount: " + nightmare.NightmareData.KillCount);
    }

    private void Start()
    {
        lastmove = new Vector2(moverRef.transform.position.x, moverRef.transform.position.y);
    }

    public void Update()
    {
        if (lastInstantiate != null)
        {
            float move = 0;
            if (moverRef.transform.position.x > lastmove.x)
            {
                move = 1;
            }
            else if (moverRef.transform.position.x < lastmove.x)
            {
                move = -1;
            }
            else
            {
                move = 0;
            }
            lastmove = new Vector2(moverRef.transform.position.x, moverRef.transform.position.y);
            lastInstantiate.transform.eulerAngles -= new Vector3(0, move * 4, 0);
        }
    }

    public void DestroyModel()
    {
        if (lastInstantiate != null)
        {
            Destroy(lastInstantiate);
        }
    }
}
