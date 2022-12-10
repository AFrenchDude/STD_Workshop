using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Made by Melinon Remy
public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private List<Image> stars;
    [SerializeField] private GameObject newRecordImage;
    private int score = 0;
    private int scoreReached;
    private bool isDisplaying = false;

    private int starCompar = 0;
    private bool isCheckingForBestScore = true;

    //On activation
    public void Activate()
    {
        scoreReached = LevelReferences.Instance.ScoreManager.score;
        StartCoroutine(LittleWait());
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
        if(isDisplaying && score <= scoreReached)
        {
            //Set score text
            text.SetText("Score: " + score);
            //If on win screen
            if(stars.Count > 0)
            {
                for(int i = 0; i != LevelReferences.Instance.ScoreManager.starScore.Count; i++)
                {
                    //Check for star
                    if (score >= LevelReferences.Instance.ScoreManager.starScore[i] && stars[i].gameObject != null)
                    {
                        stars[i].gameObject.SetActive(true);
                        GiveStar(i + 1);
                    }
                }
            }
            //Increase score
            score += 2;
        }
        if (score >= scoreReached && isCheckingForBestScore)
        {
            isCheckingForBestScore = false;
            LevelReferences.Instance.ScoreManager.levelSave.CheckForNewRecord(scoreReached, out bool gotBool);
            if (gotBool)
            {
                newRecordImage.SetActive(true);
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
