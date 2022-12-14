//By ALBERT Esteban & Dorian ALEXANDRE (Pour 2 lignes)
using UnityEngine;
using System.Collections.Generic;

public class EndGameCondition : Singleton<EndGameCondition>
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] private List<GameObject> victoryScreen = new List<GameObject>();
    
    protected override void OnEnable()
    {
        base.OnEnable();
        Base baseInstance = Base.Instance;
        baseInstance.BaseDied.RemoveListener(PlayerDefeat);
        baseInstance.BaseDied.AddListener(PlayerDefeat);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Base.Instance.BaseDied.RemoveListener(PlayerDefeat);
    }
    public void PlayerVictory()
    {
        Time.timeScale = 1.0f;
        LevelReferences.Instance.Player.GetComponent<GoldManager>().ExportPurchaseToCSV();
        foreach(GameObject p in victoryScreen)
        {
            p.SetActive(true);
            if(p.TryGetComponent(out ScoreText scoreText) == true)
            {
                scoreText.Activate();
            }
            
        }
        
    }
    public void PlayerDefeat()
    {
        Time.timeScale = 1.0f;
        LevelReferences.Instance.Player.GetComponent<GoldManager>().ExportPurchaseToCSV();

        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<ScoreText>().Activate();
    }
}
