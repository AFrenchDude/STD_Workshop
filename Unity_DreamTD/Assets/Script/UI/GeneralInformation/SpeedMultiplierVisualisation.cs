using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SpeedMultiplierVisualisation : MonoBehaviour
{
    [SerializeField]
    private Color _textUnpressColor;
    [SerializeField]
    private Color _textPressColor;

    [Space(20)]

    [SerializeField]
    private List<SpeedButton> _speedButtons = new List<SpeedButton>();


    private void Awake()
    {
        ActiveButton(0);    
    }

    public void ActiveButton(int index)
    {
        if(index < _speedButtons.Count & index >= 0)
        {
            for(int i = 0; i < _speedButtons.Count; i++)
            {
                if(i == index)
                {
                    PressButton(_speedButtons[i]);
                }
                else
                {
                    UnpressButton(_speedButtons[i]);
                }
            }
        }
            
    }

    public void PressButton(SpeedButton speedButton)
    {
        speedButton.image.sprite = speedButton.pressedButton;
        speedButton.text.color = _textPressColor;
    }

    public void UnpressButton(SpeedButton speedButton)
    {
        speedButton.image.sprite = speedButton.unpressedButton;
        speedButton.text.color = _textUnpressColor;
    }


    [System.Serializable]
    public class SpeedButton
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private Sprite _unpressedButton;
        [SerializeField]
        private Sprite _pressedButton;

        public Image image => _image;
        public TextMeshProUGUI text => _text;
        public Sprite unpressedButton => _unpressedButton;
        public Sprite pressedButton => _pressedButton;
    }
}
