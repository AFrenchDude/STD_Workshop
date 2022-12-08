using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        goldManager = LevelReferences.Instance.Player.GetComponent<GoldManager>();
        _animator = GetComponent<Animator>();
    }

    public void CreatePanel(FactoryManager towerManager)
    {
        _factoryManager = towerManager;
        transform.GetComponent<FollowOnScreen>().SetTarget(towerManager.CenterOfMass);
    }

    public void DestroyPanel()
    {
        if (_animator.GetBool("Close"))
        {

            if (factoryInformation != null)
            {
                factoryInformation.transform.parent = transform.parent;
                factoryInformation.FadeOut();
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
        ClosePanel();
    }

    public void Upgrade()
    {


        UsineBehaviour factoryScriptRef = _factoryManager.transform.GetComponent<UsineBehaviour>();
        if (goldManager.getFortune >= _factoryManager.FactoryData.CurrentUpgrade.UpgradePrice & _factoryManager.FactoryData.CurrentUpgrade.NextUpgrade != null)
        {
            string purchaseName = _factoryManager.FactoryData.name + "_Upgrade_" + _factoryManager.FactoryData.CurrentUpgrade.name;
            goldManager.Buy(_factoryManager.FactoryData.CurrentUpgrade.UpgradePrice, purchaseName);

            _factoryManager.FactoryData.Upgrade();
            _factoryManager.ApplyStats(_factoryManager.FactoryData);


            factoryScriptRef.SetUpgradeMesh(_factoryManager.FactoryData.CurrentUpgrade.UpgradePrefab);

            if (factoryInformation != null)
            {
                factoryInformation.SetFactoryData(_factoryManager.FactoryData);

            }
        }
    }

    //Informations

    private UI_FactoryPanelManager factoryInformation;

    public void CreateFactoryUpgradeInformation()
    {
        if (factoryInformation == null)
        {
            factoryInformation = Instantiate(_factoryInfoPrefab, _infoParent);

            factoryInformation.SetFactoryData(_factoryManager.FactoryData);

        }
    }

    public void RemoveFactoryUpgradeInformation()
    {
        factoryInformation.FadeOut();
    }
}
