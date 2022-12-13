using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Made by Melinon Remy
public class ScoreText : MonoBehaviour
{
    [SerializeField] bool isVictory = false;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private List<Image> stars;
    [SerializeField] private GameObject newRecordImage;
    [SerializeField] private List<GameObject> objectsToDisableOnVictory;

    //Score
    private int visualScore = 0;
    private int scoreReached;
    float lerp = 0f;
    float duration = 2.5f;
    private bool isDisplaying = false;
    private bool gotNewHighscore = false;

    //Level save
    private int starCompar = 0;
    private bool isCheckingForBestScore = true;

    private void Start()
    {
        List<int> starScore = LevelReferences.Instance.ScoreManager.levelSave.starScore;
        for (int i = 0; i < starScore.Count; i++)
        {
            if (LevelReferences.Instance.ScoreManager.score >= starScore[i])
            {
                GiveStar(i + 1); //Method takes an index, not a number of stars to add
                Debug.LogWarning("New Star added");
            }
        }
        LevelReferences.Instance.ScoreManager.levelSave.CheckForNewRecord(scoreReached, out bool gotBool);
        gotNewHighscore = gotBool;
        Debug.LogWarning("New Highscore?: " + gotBool);
    }


    //On activation
    public void Activate()
    {
        foreach (var gameobject in objectsToDisableOnVictory)
        {
            gameobject.SetActive(false);
        }
        if (isVictory)
        {
            scoreReached = LevelReferences.Instance.ScoreManager.score;
            StartCoroutine(LittleWait());
        }
    }
    private IEnumerator LittleWait()
    {
        isDisplaying = false;
        yield return new WaitForSeconds(1);
        //Start increase number
        isDisplaying = true;
    }

    //Score display increase
    private void Update()
    {
        if (isVictory)
        {
            if (isDisplaying && visualScore <= scoreReached)
            {
                lerp += Time.deltaTime / duration;
                visualScore = (int)Mathf.Lerp(0, scoreReached, lerp);
                text.SetText("Score: " + visualScore);
                //If on win screen
                if (stars.Count > 0)
                {
                    for (int i = 0; i != LevelReferences.Instance.ScoreManager.levelSave.starScore.Count; i++)
                    {
                        //Check for star
                        if (visualScore >= LevelReferences.Instance.ScoreManager.levelSave.starScore[i] && stars[i].gameObject != null)
                        {
                            stars[i].color = new Color(255, 255, 255);
                            //GiveStar(i + 1);
                        }
                    }
                }
            }
            //Best score
            if (visualScore >= scoreReached && isCheckingForBestScore)
            {
                isCheckingForBestScore = false;
                newRecordImage.SetActive(gotNewHighscore);
            }
        }
    }

    void GiveStar(int starToGive)
    {
        if (starToGive > starCompar)
        {
            starCompar++;
            LevelReferences.Instance.ScoreManager.levelSave.CheckToAddStar(starToGive);
        }
    }
}
