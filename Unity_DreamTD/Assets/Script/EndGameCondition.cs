//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCondition : Singleton<EndGameCondition>
{
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
        Debug.Log("Player won");
    }
    public void PlayerDefeat()
    {
        Debug.Log("Player lost");
    }
}
