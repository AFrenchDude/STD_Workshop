//From Template
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

public enum EntityType
{
    None,
    Veggie_Default,
    Veggie_Flying,
    Veggie_Sabotage,
    Ghost_Default,
    Ghost_Flying,
    Ghost_Sabotage,
    Insect_Default,
    Insect_Flying,
    Insect_Sabotage
}


[CreateAssetMenu(menuName = "DreamTD/WaveDatabase")]
public class WaveDatabase : ScriptableObject
{
    [SerializeField]
    private List<WaveEntityData> _waveEntityDatas = null;

    [SerializeField]
    private List<WaveSet> _waves = null;

    public List<WaveSet> Waves
    {
        get { return _waves; }
    }

    public bool GetWaveElementFromType(NightmareData nightmareData, out WaveEntity outEntity)
    {
        WaveEntityData waveEntityData = _waveEntityDatas.Find(entity => entity.NightmareType == nightmareData.nighmareType & entity.NightmareFunction == nightmareData.nightmareFunction);
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
        for (int i = 0, length = waves.Count; i < length; i++)
        {
            duration += waves[i].GetWaveDuration();
        }
        return duration;
    }
}
#endif //UNITY_EDITOR
