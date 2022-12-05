using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Transform _enemiesHealthBarContainer;

    [SerializeField]
    private GameObject _healthBarPrefab;


    public EnemiesHealthBar CreateEnemiesHealthBar(Transform target)
    {

        GameObject healthBar = Instantiate(_healthBarPrefab, _enemiesHealthBarContainer);

        healthBar.GetComponent<FollowOnScreen>().SetTarget(target);
        EnemiesHealthBar enemiesHealthBar = healthBar.GetComponent<EnemiesHealthBar>();
        return enemiesHealthBar;

    }
}
