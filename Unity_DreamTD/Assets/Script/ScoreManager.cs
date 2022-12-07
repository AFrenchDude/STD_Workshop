using UnityEngine;

//Made by Melinon Remy
public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public int oneStarScore;
    public int twoStarScore;
    public int threeStarScore;

    private void Start()
    {
        score = 0;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
