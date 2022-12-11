using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class LevelButtonSetting : MonoBehaviour
{
    //Level source
    [SerializeField] private LevelSave levelSave;

    //Display save on
    [SerializeField] private Image imageLevel; 
    [SerializeField] private GameObject imagesStars; 
    [SerializeField] private TextMeshProUGUI bestScoreText;

    //Star images ref
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite Star;

    //Set
    void Start()
    {
        bestScoreText.SetText("Best Score: " + levelSave.BestScore);
        imageLevel.sprite = levelSave.LevelImage;
        for (int i = 0; i != imagesStars.transform.childCount; i++)
        {
            if(levelSave.StarNumber > i + 1 && imagesStars.transform.GetChild(i).GetComponent<Image>() != null)
            {
                imagesStars.transform.GetChild(i).GetComponent<Image>().sprite = Star;
            }
            else
            {
                imagesStars.transform.GetChild(i).GetComponent<Image>().sprite = emptyStar;
            }
        }
    }
}
