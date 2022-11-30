using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    [SerializeField] private Info info = null;

    [SerializeField]
    private Image sprite = null;

    [SerializeField]
    private new TextMeshProUGUI name = null;

    [SerializeField]
    private TextMeshProUGUI type = null;

    [SerializeField]
    private TextMeshProUGUI production = null;

    [SerializeField]
    private TextMeshProUGUI capacity = null;

    [SerializeField]
    private Image moneySprite = null;

    [SerializeField]
    private TextMeshProUGUI moneyNecessary = null;

    private void Start()
    {
        sprite.sprite = info.sprite;
        name.text = info.name;
        type.text = info.type;
        production.text = "Produxtion : " + info.production.ToString() + "/s";
        capacity.text = info.capacity.ToString();
        moneySprite.sprite = info.moneySprite;
        moneyNecessary.text = info.moneyNecessary.ToString();
    }
}
