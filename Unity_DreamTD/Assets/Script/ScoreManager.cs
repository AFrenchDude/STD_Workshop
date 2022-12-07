using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    private void Start()
    {
        score = 0;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
