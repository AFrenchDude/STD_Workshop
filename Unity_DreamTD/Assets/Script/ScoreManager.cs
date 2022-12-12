using UnityEngine;

//Made by Melinon Remy
public class ScoreManager : MonoBehaviour
{
    public LevelSave levelSave;
    [HideInInspector] public int score = 0;

    private void Start()
    {
        score = 0;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
