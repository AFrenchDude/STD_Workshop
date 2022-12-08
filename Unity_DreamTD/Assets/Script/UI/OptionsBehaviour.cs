using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//Made by Melinon Remy
public class OptionsBehaviour : MonoBehaviour
{
    //Sound
    public AudioMixer audioMixer;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI sfxText;
    private float lastSFXvalue = 30;
    private float lastMusicValue = 20;
    //Video
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    private Resolution[] resolution;
    private int screenWidth = 1920;
    private int screenHeight = 1080;

    //Set vars to HUD
    public void OnOptionMenuOpen()
    {
        //Sound
        //music
        float musicSliderValue;
        audioMixer.GetFloat("Music", out musicSliderValue);
        musicSlider.value = musicSliderValue + 40;
        musicText.SetText((int)musicSlider.value * 2 + "%");
        //SFX
        float sfxSliderValue;
        audioMixer.GetFloat("SFX", out sfxSliderValue);
        sfxSlider.value = sfxSliderValue + 40;
        sfxText.SetText((int)sfxSlider.value * 2 + "%");
        //IsMute
        float getMute;
        audioMixer.GetFloat("Master", out getMute);
        muteToggle.isOn = getMute <= -80 ? true : false;

        //Video
        //Fullscreen
        fullscreenToggle.isOn = Screen.fullScreen;
        //Quality
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        //Resolution
        resolution = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIdex = 0;
        for (var i = 0; i < resolution.Length; i++)
        {
            //bool isAlreadyInOption = false;
            string option = resolution[i].width + "x" + resolution[i].height;
            /*
            foreach (string alreadyInOption in options)
            {
                if (option == alreadyInOption)
                {
                    isAlreadyInOption = true;
                }
            }
            if(!isAlreadyInOption)
            {
                options.Add(option);
            }
            */
            options.Add(option);

            if (resolution[i].width == Screen.currentResolution.width && resolution[i].height == Screen.currentResolution.height)
            {
                currentResolutionIdex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIdex;
        resolutionDropdown.RefreshShownValue();
    }

    //Sound
    public void Mute(bool isMuted)
    {
        if (isMuted)
        {
            audioMixer.SetFloat("Master", -80);
            StartCoroutine(MuteCoroutine());
        }
        else if(gameObject.activeSelf == true)
        {
            audioMixer.SetFloat("Master", 0);
            StartCoroutine(UnmuteCoroutine());
        }
    }
    //Wait to avoid earing half of the click sound
    IEnumerator MuteCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        sfxSlider.value = -40;
        musicSlider.value = -40;
    }
    IEnumerator UnmuteCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        sfxSlider.value = lastSFXvalue;
        musicSlider.value = lastMusicValue;
    }

    public void SetMusicVolume(Single musicVolume)
    {
        audioMixer.SetFloat("Music", musicVolume - 40);
        musicText.SetText((int)musicVolume*2 + "%");
        if (musicVolume <= 0)
        {
            audioMixer.SetFloat("Music", -80);
        }
        else
        {
            lastMusicValue = musicSlider.value;
            muteToggle.isOn = false;
        }
    }
    public void SetSFXVolume(Single sfxVolume)
    {
        audioMixer.SetFloat("SFX", sfxVolume - 40);
        sfxText.SetText((int)sfxVolume*2 + "%");
        if (sfxVolume <= 0)
        {
            audioMixer.SetFloat("SFX", -80);
        }
        else
        {
            lastSFXvalue = sfxSlider.value;
            muteToggle.isOn = false;
        }
    }

    //Video
    public void Fullscreen(bool isFullscreen)
    {
        Screen.SetResolution(screenWidth, screenHeight, isFullscreen);
    }
    public void SetResolution(int choice)
    {
        Resolution resolutionPick = resolution[choice];
        Screen.SetResolution(resolutionPick.width, resolutionPick.height, Screen.fullScreen);
        screenWidth = resolutionPick.width;
        screenHeight = resolutionPick.height;
    }
    public void SetQuality(int choice)
    {
        QualitySettings.SetQualityLevel(choice);
    }
}
