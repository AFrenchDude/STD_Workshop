using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemiesTypePreview : MonoBehaviour
{
    [SerializeField]
    private Image _icon;

    [SerializeField]
    private TextMeshProUGUI _ammountText;

    public void SetWaveData(Sprite icon, int quantities)
    {
        _icon.sprite = icon;
        _ammountText.text = "x" + quantities.ToString();
    }
}
