using UnityEngine;

//Made by Melinon Remy
public class TrainLevel : MonoBehaviour
{
    private Locomotive locomotive;
    public int currentLevel = 1;
    public int maxLevel = 3;
    public int scoreToGiveOnUpgrade = 20;

    private void Start()
    {
        locomotive = GetComponent<Locomotive>();
    }
}
