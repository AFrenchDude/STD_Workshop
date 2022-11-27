using UnityEngine;

public class TrainLevel : MonoBehaviour
{
    private Locomotive locomotive;
    public int currentLevel = 1;
    public int maxLevel = 3;

    private void Start()
    {
        locomotive = GetComponent<Locomotive>();
    }
}
