//By ALBERT Esteban, for testing purpose
using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldTxt = null;

    private void Start()
    {
        UpdateText(LevelReferences.Instance.Player.GetComponent<GoldManager>()._currentFortune);
    }
    private void UpdateText(int newGold)
    {
        if (Base.HasInstance)
        {
            _goldTxt.SetText("Gold: " + newGold);
        }
    }
}
