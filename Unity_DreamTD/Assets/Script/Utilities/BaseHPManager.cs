using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class BaseHPManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeText;
    //[SerializeField] private Slider lifeBar;
    //[SerializeField] private Image lifeBarFill;

    public void OnLifeChange(int newLife)
    {
        lifeText.SetText(newLife.ToString());

    }
}
