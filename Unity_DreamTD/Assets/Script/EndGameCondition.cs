//By ALBERT Esteban
using UnityEngine;

public class EndGameCondition : Singleton<EndGameCondition>
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject victoryScreen;

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
        victoryScreen.SetActive(true);
        victoryScreen.GetComponent<ScoreText>().Activate();
    }
    public void PlayerDefeat()
    {
        Time.timeScale = 1.0f;
        LevelReferences.Instance.Player.GetComponent<GoldManager>().ExportPurchaseToCSV();

        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<ScoreText>().Activate();
    }
}
