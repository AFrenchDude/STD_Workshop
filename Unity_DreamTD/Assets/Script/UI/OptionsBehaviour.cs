using System;
using TMPro;
using UnityEngine;

public class OptionsBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    public void Mute(bool isMuted)
    {

    }

    public void SetMusicVolume(Single musicVolume)
    {

    }
    public void SetSFXVolume(Single sfxVolume)
    {

    }

    public void Fullscreen(bool isFullscreen)
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, isFullscreen);
    }
    public void SetResolution(Int32 choice)
    {
        int screenRes = resolutionDropdown.value;
        Debug.Log(screenRes);
        //Screen.SetResolution();
    }
}
