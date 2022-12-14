using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Alexandre Dorian
public class WavePreviewElement : MonoBehaviour
{
    private WaveSet associateWave;

    private Dictionary<NightmareData, int> _nightmaresInWave = new Dictionary<NightmareData, int>();
    private List<NightmareData> _nightmareRefForDictionnary = new List<NightmareData>();

    [SerializeField]
    private EnemiesTypePreview _enemiesTypepreviewPrefab;

    public void SetWave(WaveSet wave)
    {
        associateWave = wave;
        ExtractWaveDatas();
    }

    private int index = 0;
    public void ExtractWaveDatas()
    {
        foreach(Wave wave in associateWave.Waves)
        {
            index++;
            foreach(WaveEntityDescription waveEntity in wave.WaveEntitiesDescription)
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

        CreateWavePreview();
    }

    public void CreateWavePreview()
    {
        foreach(NightmareData data in _nightmareRefForDictionnary)
        {
            EnemiesTypePreview enemiesTypePreview = Instantiate(_enemiesTypepreviewPrefab, transform);

            enemiesTypePreview.SetWaveData(data.icon, _nightmaresInWave[data]);
        }

        //Etat mental : Check
        //Etat physique : Check
        //Fatigue : Critique (Veuillez redemarrer)
    }
}
