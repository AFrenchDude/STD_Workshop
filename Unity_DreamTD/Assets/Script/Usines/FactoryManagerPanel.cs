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

    [SerializeField]
    private GameObject _infoCurrentFactoryPrefab = null;
    private GameObject _infoCurrentFactoryInstance = null;

    private Transform _infoFactoryAnchor;
    private float _infoFactorySetupTime = 0.0f;
    private bool _isWaitingForInfoFactory = false;

    private GoldManager goldManager;
    private Animator _animator;
    [SerializeField]
    private GameObject currentPanel = null;

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

    //public UnityEvent<bool> updateUpgradefactory;

    private void Awake()
    {
        _isWaitingForInfoFactory = false;
        goldManager = LevelReferences.Instance.Player.GetComponent<GoldManager>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        goldManager.FortuneChanged.RemoveListener(UpdateTowerUpgradePossibility);
        goldManager.FortuneChanged.AddListener(UpdateTowerUpgradePossibility);
    }
    private void OnDisable()
    {
        goldManager.FortuneChanged.RemoveListener(UpdateTowerUpgradePossibility);
    }

    private void Update()
    {
        //if (_isWaitingForInfoFactory)
        //{
        //    Debug.Log("Is Waiting");
        //    if (Time.time >= _infoFactorySetupTime + 1f)
        //    {
        //        currentPanel = Instantiate(_infoCurrentFactoryPrefab, _infoFactoryAnchor);
        //        Debug.Log("Has setup panel: " + currentPanel);
        //        SetInfoFactory();
        //        _isWaitingForInfoFactory = false;
        //    }
        //}
    }

    public void CreatePanel(FactoryManager towerManager)
    {
        _factoryManager = towerManager;
        _factoryManager.GetComponent<UsineBehaviour>().SetFactoryManagerPanel(this);


        SetProjectileContained(towerManager.FactoryData);

        transform.GetComponent<FollowOnScreen>().SetTarget(towerManager.CenterOfMass);

        UpdateTowerUpgradePossibility();
    }

    public void DestroyPanel()
    {
        if (_animator.GetBool("Close"))
        {
            //Debug.Log("Try destroy: " + (_infoFactoryAnchor.GetChild(_infoFactoryAnchor.childCount - 1).gameObject));

            if (factoryInformation != null)
            {
                factoryInformation.transform.parent = transform.parent;
                if (factoryInformation.gameObject.activeSelf == true)
                {
                    factoryInformation.FadeOut();
                }

            }
            Destroy(gameObject);
        }
    }

    public void ClosePanel()
    {
        Debug.Log("Try my destroy: " + (currentPanel.gameObject));
        Destroy(currentPanel.gameObject);

        _animator.SetBool("Close", true);
    }

    public void SellTower()
    {
        goldManager.CollectMoney(_factoryManager.FactoryData.CurrentUpgrade.UpgradePrice / 3);
        Destroy(_factoryManager.gameObject);
        //updateUpgradefactory.Invoke(false);
        //Debug.Log("selling");
        ClosePanel();
    }

    public void UpdateTowerUpgradePossibility()
    {
        if (factoryHasUpgrade)
        {
            _upgradeImage.sprite = _upgradeSprite;

            if (canBuyFactory)
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
        factoryInformation?.CanUpgrade(canBuyFactory);
    }

    public bool canBuyFactory
    {
        get { return _factoryManager.FactoryData.CurrentUpgrade.UpgradePrice <= goldManager.getFortune; }
    }

    public bool factoryHasUpgrade
    {
        get { return _factoryManager.FactoryData.canUpgrade; }
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
                UpdateTowerUpgradePossibility();
            }
        }

        SetInfoFactory();
        //updateUpgradefactory.Invoke(true);
    }

    public void SetInfoFactoryAnchor(Transform infoFactoryAnchor) // See HUDWhen Select calls
    {
        _infoFactoryAnchor = infoFactoryAnchor;
        ClearOtherInfoCurrentFactory();
        _infoFactorySetupTime = Time.time;
        _isWaitingForInfoFactory = true;
        currentPanel = Instantiate(_infoCurrentFactoryPrefab, _infoFactoryAnchor);


        //Debug.Log("Instantiated object? " + currentPanel);
        SetInfoFactory();


        //currentPanel.transform.parent = _infoFactoryAnchor;
        //currentPanel.transform.localPosition = new Vector3(0,0,0);
        //currentPanel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        //currentPanel.transform.localScale = new Vector3(1, 1, 1);
    }

    private void ClearOtherInfoCurrentFactory()
    {
        Debug.Log("Anchor has: " + _infoFactoryAnchor.childCount + "children");
        for (int i = 0; i < _infoFactoryAnchor.childCount; i++)
        {
            Debug.Log("Try destroy child: " + (_infoFactoryAnchor.GetChild(i).gameObject));
            if (_infoFactoryAnchor.GetChild(i).GetComponent<InfoCurrentFactory>() != null)
            {
                Destroy(_infoFactoryAnchor.GetChild(i).gameObject);
            }
        }
    }

    public void SetInfoFactory()// See HUDWhen Select
    {
        InfoCurrentFactory infoCurrentFactory = currentPanel.GetComponent<InfoCurrentFactory>();
        //Debug.Log("Set info capable? " + (infoCurrentFactory != null));

        infoCurrentFactory.Name.text = _factoryManager.FactoryData.Name;
        infoCurrentFactory.Production.text = _factoryManager.FactoryData.ProductionRate.ToString();
        infoCurrentFactory.MaxStorage.text = _factoryManager.FactoryData.MaxAmmount.ToString();
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
                factoryInformation.CanUpgrade(canBuyFactory);
            }
        }
    }

    public void RemoveFactoryUpgradeInformation()
    {
        factoryInformation.FadeOut();
    }



    //ProjectilesList

    [Header("ProjectilesList Informations")]
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
