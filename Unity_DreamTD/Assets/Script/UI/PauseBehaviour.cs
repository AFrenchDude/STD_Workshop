using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Audio;

//Made by Melinon Remy, modified by ALBERT Esteban to remove double var calls
public class PauseBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> _hudList;
    private bool _isInPause = false;
    private float _lastTimeScale;


    [SerializeField]
    private AudioMixer _audioMixer;


    [SerializeField]
    private Button _soundButton;

    [SerializeField]
    private Sprite _soundOnSprite;
    [SerializeField]
    private Sprite _soundOffSprite;

    private void Awake()
    {
        //IsMute
        float getMute;
        _audioMixer.GetFloat("Master", out getMute);

        if (getMute <= -80 ? true : false)
        {
            _soundButton.image.sprite = _soundOffSprite;
        }
        else
        {
            _soundButton.image.sprite = _soundOnSprite;
        }
    }

    public void TurnOnOffSound()
    {
        float getMute;
        _audioMixer.GetFloat("Master", out getMute);

        if (getMute <= -80 ? true : false)
        {
            _soundButton.image.sprite = _soundOnSprite;
            _audioMixer.SetFloat("Master", 0);
        }
        else
        {
            _soundButton.image.sprite = _soundOffSprite;
            _audioMixer.SetFloat("Master", -80);
        }
    }

    AudioSource _audioSourceSave;
    public void ResumeGame()
    {
        Animator animator = GetComponent<Animator>();

        if (animator.GetBool("ExitMenu") == true)
        {
            
            GetComponent<Animator>().SetBool("ExitMenu", false);
            gameObject.SetActive(!_isInPause);
            foreach (var hud in _hudList)
            {
                hud.SetActive(_isInPause);
            }
            LevelReferences.Instance.Player.GetComponentInChildren<CameraScript>().enabled = _isInPause;
            LevelReferences.Instance.Player.GetComponentInChildren<PlayerInput>().enabled = _isInPause;
            _isInPause = false;
        }
    }

    public void PauseTime()
    {
        Animator animator = GetComponent<Animator>();

        if (animator.GetBool("ExitMenu") == false)
        {
            _lastTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
    }


    public void Pause(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            //Click sound
            audioSource.Play();
        }

        if (!_isInPause)
        {
         
            gameObject.SetActive(!_isInPause);
            foreach (var hud in _hudList)
            {
                hud.SetActive(_isInPause);
            }
            LevelReferences.Instance.Player.GetComponentInChildren<CameraScript>().enabled = _isInPause;
            LevelReferences.Instance.Player.GetComponentInChildren<PlayerInput>().enabled = _isInPause;
            //Set new pause state
            _isInPause = !_isInPause;

        }
        //Unpause
        else
        {
            Time.timeScale = _lastTimeScale;
            GetComponent<Animator>().SetBool("ExitMenu", true);
        }
    }
}
