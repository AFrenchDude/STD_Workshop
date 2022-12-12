using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoTowerManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private TowerDescription towerDescription = null;

    [SerializeField]
    private Image sprite = null;

    [SerializeField]
    private new TextMeshProUGUI name = null;

    [SerializeField]
    private TextMeshProUGUI type = null;

    [SerializeField]
    private TextMeshProUGUI damage = null;

    [SerializeField]
    private TextMeshProUGUI firerate = null;

    [SerializeField]
    private TextMeshProUGUI range = null;

    [SerializeField]
    private TextMeshProUGUI capacity = null;

    [SerializeField]
    private TextMeshProUGUI moneyNecessary = null;
    #endregion Variables

    private void Awake()
    {
        UpdateInfoPanel();
    }

    public void UpdateInfoPanel()
    {
        sprite.sprite = towerDescription.TowersDatas.Icon;
        name.text = " " + towerDescription.TowersDatas.Name;
        type.text = " " + towerDescription.TowersDatas.Type;
        damage.text = towerDescription.TowersDatas.Damage.ToString();
        firerate.text = towerDescription.TowersDatas.FireRate.ToString();
        range.text = towerDescription.TowersDatas.Range.ToString();
        capacity.text = towerDescription.TowersDatas.MaxProjectilesAmmount.ToString();
        moneyNecessary.text = towerDescription.Price + " ";
    }
}
