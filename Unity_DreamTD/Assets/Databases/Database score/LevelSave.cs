using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/LevelSave", fileName = "LevelSave")]
public class LevelSave : ScriptableObject
{
    [SerializeField] private PlayerScoreSave playerScoreSave;
    [SerializeField] private int bestScore = 0;
    [SerializeField] private int starNumber = 0;

    public void CheckToAddStar(int unlockedStar)
    {
        if(unlockedStar > starNumber)
        {
            starNumber++;
            playerScoreSave.AddNewStars(1);
        }
    }

    public void CheckForNewRecord(int actualScore, out bool isNewRecord)
    {
        if (actualScore > bestScore)
        {
            bestScore = actualScore;
            isNewRecord = true;
        }
        else
        {
            isNewRecord = false;
        }
    }
}
