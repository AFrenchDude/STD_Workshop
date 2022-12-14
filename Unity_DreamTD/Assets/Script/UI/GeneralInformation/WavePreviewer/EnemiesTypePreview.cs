using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Made by Alexandre Dorian
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
