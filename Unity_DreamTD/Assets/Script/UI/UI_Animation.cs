using System.Collections;
using UnityEngine;

//Made by Alexandre Dorian, modified by Melinon Remy
public class UI_Animation : MonoBehaviour
{
    private RectTransform _rectTransform;

    [SerializeField]
    private AnimationCurve _curve;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Vector2 _newPosition;
    private Vector2 _basePosition;

    private bool _isPlaying;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _basePosition = _rectTransform.anchoredPosition;
    }

    public void StartAnim()
    {
        _isPlaying = true;
        StartCoroutine(PlayAnim(0));
    }

    public void RevertAnim()
    {
        _isPlaying = false;
        StartCoroutine(PlayRevertAnim(0));
    }

    public IEnumerator PlayAnim(float lerp)
    {
        yield return new WaitForFixedUpdate();
        lerp += Time.deltaTime;

        _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _basePosition + _newPosition, _curve.Evaluate(lerp));

        if (lerp < 1 & _isPlaying)
        {
            StartCoroutine(PlayAnim(lerp));
        }
        else if(lerp > 1 & _isPlaying)
        {
            _rectTransform.anchoredPosition = _basePosition + _newPosition;
        }
    }

    public IEnumerator PlayRevertAnim(float lerp)
    {
        //Debug.Log("InCoroutine : " + lerp.ToString());
        yield return new WaitForFixedUpdate();
        lerp += Time.deltaTime;

        _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _basePosition, _curve.Evaluate(lerp));

        if (lerp < 1 & !_isPlaying)
        {
            StartCoroutine(PlayRevertAnim(lerp));
        }
        else if (lerp > 1 & !_isPlaying)
        {
            _rectTransform.anchoredPosition = _basePosition;
        }
    }

    public void ResetAnim()
    {
        _rectTransform.anchoredPosition = _basePosition;
    }
}
