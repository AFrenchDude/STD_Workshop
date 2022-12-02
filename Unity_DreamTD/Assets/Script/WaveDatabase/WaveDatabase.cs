//From Template & ALEXANDRE Dorian
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

[CreateAssetMenu(menuName = "DreamTD/WaveDatabase")]
public class WaveDatabase : ScriptableObject
{
    [SerializeField]
    private List<WaveEntityData> _waveEntityDatas = null;

    [SerializeField]
    private List<WaveSet> _waves = null;

    [SerializeField]
    private float _delayBetweenWave;

    public List<WaveSet> Waves
    {
        get { return _waves; }
    }

    public float DelayBetweenWave => _delayBetweenWave;

    public bool GetWaveElementFromType(NightmareData nightmareData, out WaveEntity outEntity)
    {
        WaveEntityData waveEntityData = _waveEntityDatas.Find(entity => entity.NightmareData.nighmareType == nightmareData.nighmareType);
        if (waveEntityData != null)
        {
            outEntity = waveEntityData.WaveEntityPrefab;           
            return true;
        }
        outEntity = null;
        return false;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(WaveDatabase))]
public class WaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.Space(24);

        var waveDatabase = (serializedObject.targetObject as WaveDatabase);
        EditorGUILayout.TextArea(string.Format("Total Duration : {0} seconds", GetWaveDuration(waveDatabase.Waves).ToString()));
    }

    private float GetWaveDuration(List<WaveSet> waves)
    {
        float duration = 0;
        float delayBtwWave = WaveDatabaseManager.Instance.WaveDatabase.DelayBetweenWave;
        for (int i = 0, length = waves.Count; i < length; i++)
        {
            duration += waves[i].GetWaveDuration();
            duration += delayBtwWave;
        }
        return duration;
    }
}
#endif //UNITY_EDITOR
