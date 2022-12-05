using UnityEngine;

//Made by Melinon Remy
public class PauseBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    private bool isInPause = false;
    [SerializeField] private Controller controller;
    public void Pause(AudioSource audioSource)
    {
        audioSource.Play();
        //Unpause
        if (isInPause)
        {
            gameObject.SetActive(false);
            HUD.SetActive(true);
            Time.timeScale = 1f;
            controller.isInPause = false;
        }
        //Pause
        else
        {
            gameObject.SetActive(true);
            HUD.SetActive(false);
            Time.timeScale = 0f;
            controller.isInPause = true;
        }
        isInPause = !isInPause;
    }
}
