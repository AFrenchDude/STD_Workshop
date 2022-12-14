using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

//Made by Alexandre Dorian
public class WaveOriginPreview : MonoBehaviour
{
    [SerializeField]
    private List<VisualEffect> effects = new List<VisualEffect>();


    public void SetFireActivation(bool isActive)
    {
        Debug.Log(isActive);
        if (isActive)
        {
            foreach(VisualEffect effect in effects)
            {
                effect.playRate = 1;
            }
        }

    }

    public void ResetFireActivation()
    {
        foreach (VisualEffect effect in effects)
        {
            effect.playRate = 0;
        }
    }
}
