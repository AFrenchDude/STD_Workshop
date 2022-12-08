using UnityEngine;
using UnityEngine.UI;

public class MuseumButton : MonoBehaviour
{
    [SerializeField] private NightmareBestiaryData nightmareBestiary;
    public NightmareBestiaryData NightmareBestiary => nightmareBestiary;

    private void Start()
    {
        if(nightmareBestiary.NightmareData.icon != null)
        {
            GetComponent<Image>().sprite = nightmareBestiary.NightmareData.icon;
        }
    }
}
