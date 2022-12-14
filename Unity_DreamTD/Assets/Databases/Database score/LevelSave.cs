using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "DreamTD/LevelSave", fileName = "LevelSave")]
public class LevelSave : ScriptableObject
{
    [Header("Name")]
    [SerializeField] private string _levelName;

    [Header("Info")]
    [SerializeField] private PlayerScoreSave playerScoreSave;
    [SerializeField] private int bestScore = 0;
    [SerializeField] private int starNumber = 0;

    [SerializeField] private Sprite levelImage;

    [SerializeField] private bool _isLock;

    public string LevelName => _levelName;
    public int BestScore => bestScore;
    public int StarNumber => starNumber;
    public Sprite LevelImage => levelImage;

    public bool IsLock => _isLock;

    //Score to reach to get stars
    public List<int> starScore;

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

    public void SetIsLock(bool isLock)
    {
        _isLock = isLock;
    }
}
