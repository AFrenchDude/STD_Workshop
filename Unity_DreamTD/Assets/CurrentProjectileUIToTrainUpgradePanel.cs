using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentProjectileUIToTrainUpgradePanel : MonoBehaviour
{
    [SerializeField]
    private CurrentProjectileUI _currentProjectileUI = null;

    [SerializeField]
    private int _wagonIndex = 0;

    public void SendInformationToTrainUpgradePanel()
    {
        //_currentProjectileUI.Projectile.ProjectileType;
        GetComponentInParent<TrainUpgradePanel>().SetNewProjectileType(_wagonIndex, _currentProjectileUI.Projectile.ProjectileType);
    }

    private void OnEnable()
    {
        _currentProjectileUI.ProjectileCreated.RemoveListener(SendInformationToTrainUpgradePanel);
        _currentProjectileUI.ProjectileCreated.AddListener(SendInformationToTrainUpgradePanel);

    }

    private void OnDisable()
    {
        _currentProjectileUI.ProjectileCreated.RemoveListener(SendInformationToTrainUpgradePanel);

    }
}
