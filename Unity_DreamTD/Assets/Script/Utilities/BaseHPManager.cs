using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class BaseHPManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private Slider lifeBar;
    [SerializeField] private Image lifeBarFill;

    public void OnLifeChange(int newLife)
    {
        lifeText.SetText("Life : " + newLife);
        lifeBar.value = (float)newLife / 100;
        if(newLife > 50)
        {
            lifeBarFill.color = Color.green;
        }
        if(newLife <= 50 && newLife > 10)
        {
            lifeBarFill.color = Color.yellow;
        }
        else if(newLife <= 10)
        {
            lifeBarFill.color = Color.red;
        }
    }
}
