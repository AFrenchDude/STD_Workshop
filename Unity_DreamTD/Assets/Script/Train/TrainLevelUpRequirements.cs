//By ALBERT Esteban
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamTD/TrainUpgradeLevelData", fileName = "TrainUpgradeLevelData")]
public class TrainLevelUpRequirements : ScriptableObject
{
    [SerializeField] private List<TrainUpgradeStructure> _trainLevelUpList = new List<TrainUpgradeStructure>();
    [SerializeField] private List<GameObject> _locomotiveMeshes;
    private int _levelUpIndex = 0;
    private int _locoMeshIndex = 0;

    [Serializable]
    public class TrainUpgradeStructure
    {
        public int waveToSurvive;
        public bool upgradeLocoSpeed;
        public bool upgradeWagonCount;
        public bool upgradeWagonStorage;
        public bool upgradeLocoMesh;

        public TrainUpgradeStructure(int aWaveToSurvive, bool aUpgradeLocoSpeed, bool aUpgradeWagonCount, bool aUpgradeWagonStorage, bool aUpgradeLocoMesh)
        {
            waveToSurvive = aWaveToSurvive;
            upgradeLocoSpeed = aUpgradeLocoSpeed;
            upgradeWagonCount = aUpgradeWagonCount;
            upgradeWagonStorage = aUpgradeWagonStorage;
            upgradeLocoMesh = aUpgradeLocoMesh;
        }
    }

    public TrainUpgradeStructure GetLevelUpAction()
    {
        return _trainLevelUpList[_levelUpIndex];
    }

    public int GetNextLevelUpRequirement()
    {
        return _trainLevelUpList[_levelUpIndex].waveToSurvive;
    }

    public void LevelUp(out bool hasANextUpgrade)
    {
        hasANextUpgrade = true;

        if (_trainLevelUpList[_levelUpIndex].upgradeLocoMesh)
        {
            _locoMeshIndex++;
        }
        _levelUpIndex++;

        if (_levelUpIndex > _trainLevelUpList.Count - 1)
        {
            hasANextUpgrade = false;
        }
    }

}
