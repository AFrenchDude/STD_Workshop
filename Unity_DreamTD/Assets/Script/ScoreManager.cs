using UnityEngine;
using System.Collections.Generic;

//Made by Melinon Remy
public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    //Score to reach to get stars
    public List<int> starScore;

    private void Start()
    {
        score = 0;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
