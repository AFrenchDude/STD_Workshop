//By ALBERT Esteban, for testing purpose
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldTxt = null;
    private void OnEnable()
    {
        Base.Instance.GoldUpdated.RemoveListener(UpdateText);
        Base.Instance.GoldUpdated.AddListener(UpdateText);
    }
    private void OnDisable()
    {
        Base.Instance.GoldUpdated.RemoveListener(UpdateText);
    }

    private void Start()
    {
        UpdateText(Base.Instance.Gold);
        
    }
    private void UpdateText(int newGold)
    {
        _goldTxt.SetText("Gold: " + newGold);
    }
}
