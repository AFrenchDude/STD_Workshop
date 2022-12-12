//By Dorian ALEXANDRE 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

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
    private List<FactoryDatas> _allFactoryRef = new List<FactoryDatas>();

    [SerializeField]
    private GoldManager goldManager;

    private int index;
    private void Awake()
    {
        index = 0;
        //goldManager = LevelReferences.Instance.Player.GetComponent<GoldManager>();
        _currentWaveData = new WaveData();
        CreateNewWave(0);
    }

    //Senders
    string filename = "";
    public void SetFilename(TextMeshProUGUI name)
    {
        _name = name.text;

    }

    public void SendData()
    {
        if (_name == null)
        {
            _name = "Unknow";
        }

        string date = System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString() + "_" + System.DateTime.Now.Hour.ToString() + "h" + System.DateTime.Now.Minute.ToString();

        filename = Application.dataPath + "/WaveTestDatabase" + "/level_01_" + _name + "_" + date + ".csv";

        ExportTestDataToCsv();
    }

    [ContextMenu("ExportPurchaseToCSV")]
    public void ExportTestDataToCsv()
    {
        if (filename == "")
        {
            string date = System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString() + "_" + System.DateTime.Now.Hour.ToString() + "h" + System.DateTime.Now.Minute.ToString();
            filename = Application.dataPath + "/WaveTestDatabase" + "/level_01_" + "Unfinished" + "_" + date + ".csv";
        }

        if (_waves.Count > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Wave, Begin Fortune, EndFortune Fortune, Begin Life, End Life, Enemies in wave, Enemies Quantity, Tower Datas");
            tw.Close();

            tw = new StreamWriter(filename, true);

            for (int i = 0; i < _waves.Count; i++)
            {
                string baseString = _waves[i].waveIndex.ToString() + "," + _waves[i].beginFortune.ToString() + "," + _waves[i].endFortune.ToString() + "," + _waves[i].beginLife.ToString() + "," + _waves[i].endLife.ToString();
                tw.WriteLine(baseString);

                if (_waves[i].enemiesOfWave.Count > 0)
                {
                    foreach (WaveEnemiesData enemies in _waves[i].enemiesOfWave)
                    {
                        tw.WriteLine(baseString + "," + enemies.enemyType.name + "," + enemies.enemiesQuantities);
                    }
                }

                if (_waves[i].towerOfWave.Count > 0)
                {
                    foreach (string tower in _waves[i].towerOfWave)
                    {
                        tw.WriteLine(baseString + "," + " " + "," + " " + "," + tower);
                    }
                }
            }
            tw.Close();

            Debug.Log("Data save to : " + Application.dataPath);
        }
    }

    //Methods
    public void CreateNewWave(int index)
    {
        Debug.Log(index);
        if (index > 0)
        {
            EndWave();
        }
        _waves.Add(_currentWaveData);

        _currentWaveData = new WaveData();
        _currentWaveData.enemiesOfWave = new List<WaveEnemiesData>();
        _currentWaveData.towerOfWave = new List<string>();

        if (index < WaveDatabaseManager.Instance.WaveDatabase.Waves.Count)
        {

            WaveSet waveSet = WaveDatabaseManager.Instance.WaveDatabase.Waves[index];
        }

        //Set Up Current Wave
        _currentWaveData.waveIndex = index;

        //Set Up Fortune
        _currentWaveData.beginFortune = goldManager.getFortune;

        //Set up Life
        _currentWaveData.beginLife = Base.Instance.BaseHP;

        if (index > 0)
        {

            WaveSet currentWaveSet = WaveDatabaseManager.Instance.WaveDatabase.Waves[index];

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
        //Set up Factory
        foreach (FactoryDatas factory in _allFactoryRef)
        {
            Debug.Log(_currentWaveData.towerOfWave);
            _currentWaveData.towerOfWave.Add(factory.CurrentUpgrade.name);
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
    public void AddFactory(FactoryDatas factoryDatas)
    {
        _allFactoryRef.Add(factoryDatas);
    }



    [System.Serializable]
    public class WaveData
    {
        public int waveIndex;

        public int beginFortune;
        public int endFortune;

        public int beginLife;
        public int endLife;

        public List<string> towerOfWave = new List<string>();

        public List<WaveEnemiesData> enemiesOfWave = new List<WaveEnemiesData>();

    }

    [System.Serializable]
    public class WaveEnemiesData
    {
        public NightmareData enemyType;
        public int enemiesQuantities;
    }
}
