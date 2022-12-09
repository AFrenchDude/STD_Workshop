using System.Collections.Generic;
using UnityEngine;

//Made by Melinon Remy, modified by ALBERT Esteban to remove double var calls
public class PauseBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> _hudList;
    private bool _isInPause = false;
    private float _lastTimeScale;

    public void Pause(AudioSource audioSource)
    {
        //Click sound
        audioSource.Play();
        if (_isInPause)
        {
            Time.timeScale = _lastTimeScale;
        }
        //Unpause
        else
        {
            _lastTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        gameObject.SetActive(!_isInPause);
        foreach (var hud in _hudList)
        {
            hud.SetActive(_isInPause);
        }
        LevelReferences.Instance.Player.GetComponentInChildren<CameraScript>().enabled = _isInPause;
        //Set new pause state
        _isInPause = !_isInPause;
    }
}
