//By Dorian ALEXANDRE 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataSaver : MonoBehaviour
{
    [SerializeField]
    private List<WaveData> _waves = new List<WaveData>();

    [SerializeField]
    private WaveData _currentWaveData = new WaveData();

    private string _name;


    private Dictionary<NightmareData, int> _nightmaresInWave = new Dictionary<NightmareData, int>();
    private List<NightmareData> _nightmareRefForDictionnary = new List<NightmareData>();

    [SerializeField]
    private List<TowersDatas> _allTowersRef = new List<TowersDatas>();

    [SerializeField]
    private GoldManager goldManager;

    private void Awake()
    {
        //goldManager = LevelReferences.Instance.Player.GetComponent<GoldManager>();
        _currentWaveData = new WaveData();
        CreateNewWave(0);
    }
    //Senders
    public void SetFilename(TextMeshProUGUI name)
    {
        _name = name.text;
    }

    public void SendData()
    {
        Debug.Log(_name);
    }

    //Methods
    public void CreateNewWave(int index)
    {
        if (index > 0)
        {
            EndWave();
            _waves.Add(_currentWaveData);

        }

        _currentWaveData = new WaveData();
        _currentWaveData.enemiesOfWave = new List<WaveEnemiesData>();
        _currentWaveData.towerOfWave = new List<string>();

        WaveSet waveSet = WaveDatabaseManager.Instance.WaveDatabase.Waves[index];

        //Set Up Current Wave
        _currentWaveData.waveIndex = index;

        //Set Up Fortune
        _currentWaveData.beginFortune = goldManager.getFortune;

        //Set up Life
        _currentWaveData.beginLife = Base.Instance.BaseHP;

        if (index > 0)
        {

            WaveSet currentWaveSet = WaveDatabaseManager.Instance.WaveDatabase.Waves[index - 1];

            //Get Enemies in current wave
            foreach (Wave wave in currentWaveSet.Waves)
            {
                foreach (WaveEntityDescription waveEntity in wave.WaveEntitiesDescription)
                {
                    NightmareData data = waveEntity.NightmareData;
                    if (_nightmaresInWave.ContainsKey(data))
                    {
                        _nightmaresInWave[data]++;
                    }
                    else
                    {
                        _nightmaresInWave.Add(data, 1);
                        _nightmareRefForDictionnary.Add(data);
                    }
                }
            }

            foreach (NightmareData data in _nightmareRefForDictionnary)
            {
                WaveEnemiesData newEnemiesWave = new WaveEnemiesData();

                newEnemiesWave.enemyType = data;
                newEnemiesWave.enemiesQuantities = _nightmaresInWave[data];

                _currentWaveData.enemiesOfWave.Add(newEnemiesWave);
            }
        }

    }
    public void EndWave()
    {
        //Set up Towers
        foreach (TowersDatas tower in _allTowersRef)
        {
            Debug.Log(_currentWaveData.towerOfWave);
            _currentWaveData.towerOfWave.Add(tower.UpgradeDatas.name);
        }

        //Set Up Fortune
        _currentWaveData.endFortune = goldManager.getFortune;

        //Set up Life
        _currentWaveData.endLife = Base.Instance.BaseHP;
    }





    public void AddTurret(TowersDatas towerData)
    {
        _allTowersRef.Add(towerData);
    }



    [System.Serializable]
    public class WaveData
    {
        public int waveIndex;

        public int beginFortune;
        public int endFortune;

        public int beginLife;
        public int endLife;

        public List<string> towerOfWave;

        public List<WaveEnemiesData> enemiesOfWave;

    }

    [System.Serializable]
    public class WaveEnemiesData
    {
        public NightmareData enemyType;
        public int enemiesQuantities;
    }
}
