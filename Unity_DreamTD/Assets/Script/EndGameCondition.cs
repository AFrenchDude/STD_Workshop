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
        victoryScreen.SetActive(true);
        victoryScreen.GetComponent<ScoreText>().Activate();
    }
    public void PlayerDefeat()
    {
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<ScoreText>().Activate();
    }
}
