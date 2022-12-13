using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class FactoryManagerPanel : MonoBehaviour
{
    [SerializeField]
    private FactoryManager _factoryManager;

    [Header("Information")]
    [SerializeField]
    private UI_FactoryPanelManager _factoryInfoPrefab;

    [SerializeField]
    private Transform _infoParent;


    private GoldManager goldManager;
    private Animator _animator;

    [Header("UI Economy")]
    [SerializeField]
    private Image _upgradeImage;

    [Space(10)]

    [SerializeField]
    private Sprite _upgradeSprite;
    [SerializeField]
    private Sprite _lockedSprite;

    [Space(10)]

    [SerializeField]
    private Color _canBuyColor;
    [SerializeField]
    private Color _cantBuyColor;

    public UnityEvent<bool> updateUpgradefactory;

    private void Awake()
    {
        goldManager = LevelReferences.Instance.Player.GetComponent<GoldManager>();
        _animator = GetComponent<Animator>();
    }

    public void CreatePanel(FactoryManager towerManager)
    {
        _factoryManager = towerManager;
        _factoryManager.GetComponent<UsineBehaviour>().SetFactoryManagerPanel(this);

        SetProjectileContained(towerManager.FactoryData);

        transform.GetComponent<FollowOnScreen>().SetTarget(towerManager.CenterOfMass);
    }

    public void DestroyPanel()
    {
        if (_animator.GetBool("Close"))
        {

            if (factoryInformation != null)
            {
                factoryInformation.transform.parent = transform.parent;
                if(factoryInformation.gameObject.active == true)
                {
                    factoryInformation.FadeOut();
                }
                
            }

            Destroy(gameObject);
        }
    }

    public void ClosePanel()
    {
        _animator.SetBool("Close", true);
    }

    public void SellTower()
    {

        goldManager.CollectMoney(_factoryManager.FactoryData.CurrentUpgrade.UpgradePrice / 3);
        Destroy(_factoryManager.gameObject);
        updateUpgradefactory.Invoke(false);
        ClosePanel();
    }

    public void UpdateTowerUpgrdePossibility()
    {
        if (_factoryManager.FactoryData.CurrentUpgrade.NextUpgrade != null)
        {
            _upgradeImage.sprite = _upgradeSprite;

            if (goldManager.getFortune >= _factoryManager.FactoryData.CurrentUpgrade.UpgradePrice)
            {
                _upgradeImage.color = _canBuyColor;
            }
            else
            {
                _upgradeImage.color = _cantBuyColor;
            }
        }
        else
        {
            _upgradeImage.color = Color.white;
            _upgradeImage.sprite = _lockedSprite;
        }
    }

    public void Upgrade()
    {


        UsineBehaviour factoryScriptRef = _factoryManager.transform.GetComponent<UsineBehaviour>();
        if (goldManager.getFortune >= _factoryManager.FactoryData.CurrentUpgrade.UpgradePrice & _factoryManager.FactoryData.CurrentUpgrade.NextUpgrade != null)
        {
            string purchaseName = _factoryManager.FactoryData.name + "_Upgrade_" + _factoryManager.FactoryData.CurrentUpgrade.name;
            goldManager.Buy(_factoryManager.FactoryData.CurrentUpgrade.UpgradePrice, purchaseName);

            _factoryManager.FactoryData.Upgrade();
            //_factoryManager.ApplyStats(_factoryManager.FactoryData);




            factoryScriptRef.SetUpgradeMesh(_factoryManager.FactoryData.CurrentUpgrade.UpgradePrefab);
            factoryScriptRef.ActiveUpgradeVfx();



            if (factoryInformation != null & _factoryManager.FactoryData.CurrentUpgrade.NextUpgrade != null)
            {
                factoryInformation.SetFactoryData(_factoryManager.FactoryData);
            }
            else
            {
                UpdateTowerUpgrdePossibility();
            }
        }

        updateUpgradefactory.Invoke(true);
    }

    //Informations

    private UI_FactoryPanelManager factoryInformation;

    public void CreateFactoryUpgradeInformation()
    {
        if (_factoryManager.FactoryData.canUpgrade)
        {
            if (factoryInformation == null)
            {
                factoryInformation = Instantiate(_factoryInfoPrefab, _infoParent);

                factoryInformation.SetFactoryData(_factoryManager.FactoryData);

            }

        }
    }

    public void RemoveFactoryUpgradeInformation()
    {

        factoryInformation.FadeOut();
        
    }



    //Projectiles

    [Header("Projectiles Informations")]
    [SerializeField]
    private RectMask2D _sliderMask;
    [SerializeField]
    private Vector2 _sliderMaskMinMax;

    [SerializeField]
    private Image _sliderImage;
    [SerializeField]
    private TextMeshProUGUI _projectileAmmountText;

    [SerializeField]
    private Color _lowerColor;
    [SerializeField]
    private Color _higherColor;

    public void SetProjectileContained(FactoryDatas factoryDatass)
    {

        float lerpValue = (float)factoryDatass.Ammount / (float)factoryDatass.MaxAmmount;

        _sliderMask.padding = new Vector4(0, 0, 0, Mathf.Lerp(_sliderMaskMinMax.y, _sliderMaskMinMax.x, lerpValue));

        _sliderImage.color = Color.Lerp(_lowerColor, _higherColor, lerpValue);

        _projectileAmmountText.text = factoryDatass.Ammount.ToString();
    }


}
