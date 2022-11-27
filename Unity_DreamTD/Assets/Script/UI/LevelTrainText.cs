using TMPro;
using UnityEngine;

public class LevelTrainText : MonoBehaviour
{
    [HideInInspector] public TextMeshProUGUI text;
    public TrainLevel trainLevel;

    public void OnUpgrade()
    {
        trainLevel.currentLevel++;
        text.SetText("Level " + trainLevel.currentLevel);
    }
}
