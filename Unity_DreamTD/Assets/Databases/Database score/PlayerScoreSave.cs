using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/PlayerScore", fileName = "PlayerScore")]
public class PlayerScoreSave : ScriptableObject
{
    [SerializeField] private int starNumber;

    public int StarNumber => starNumber;

    public void AddNewStars(int starsToAdd)
    {
        starNumber += starsToAdd;
    }
    public void RemoveStars(int starsToRemove)
    {
        starNumber -= starsToRemove;
    }
}
