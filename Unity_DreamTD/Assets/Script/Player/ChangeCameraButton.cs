//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraButton : MonoBehaviour
{
    [SerializeField] private List<Camera> _cameraList = null;
    private int _cameraIndex = 0;

    public void CameraSwitch()
    {
        _cameraList[_cameraIndex].enabled = false;
        _cameraIndex++;
        if (_cameraIndex >= _cameraList.Count)
        {
            _cameraIndex = 0;
        }
        _cameraList[_cameraIndex].enabled = true;
    }
}
