using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    public void UpdateLife(float life, float maxLife)
    {
        _slider.value = life / maxLife;
    }
}
