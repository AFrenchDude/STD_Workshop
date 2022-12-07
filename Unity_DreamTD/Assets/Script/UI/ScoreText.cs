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
    private int score = 0;
    private int scoreReached;
    private bool isDisplaying = false;

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
                //Check for first star
                if (score >= LevelReferences.Instance.ScoreManager.oneStarScore)
                {
                    stars[0].gameObject.SetActive(true);
                }
                //Check for second star
                if (score >= LevelReferences.Instance.ScoreManager.twoStarScore)
                {
                    stars[1].gameObject.SetActive(true);
                }
                //Check for third star
                if (score >= LevelReferences.Instance.ScoreManager.threeStarScore)
                {
                    stars[2].gameObject.SetActive(true);
                }
            }
            //Increase score
            score += 2;
        }
    }
}
