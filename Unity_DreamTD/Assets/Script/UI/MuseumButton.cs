using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class MuseumButton : MonoBehaviour
{
    [SerializeField] private NightmareBestiaryData nightmareBestiary;
    public NightmareBestiaryData NightmareBestiary => nightmareBestiary;

    //Set button icon
    private void Start()
    {
        if(nightmareBestiary.NightmareData.icon != null)
        {
            GetComponent<Image>().sprite = nightmareBestiary.NightmareData.icon;
        }
    }
}
