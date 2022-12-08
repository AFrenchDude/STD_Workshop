using UnityEngine;

//Made by Melinon Remy
public class PauseBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    private bool isInPause = false;
    private float lastTimeScale;
    //Get controller to disable movement in pause
    [SerializeField] private Controller controller;

    public void Pause(AudioSource audioSource)
    {
        //Click sound
        audioSource.Play();
        //Pause
        if (isInPause)
        {
            gameObject.SetActive(false);
            HUD.SetActive(true);
            Time.timeScale = lastTimeScale;
            //Disable movement
            controller.isInPause = true;
        }
        //Unpause
        else
        {
            gameObject.SetActive(true);
            HUD.SetActive(false);
            lastTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            //Enable movement
            controller.isInPause = false;
        }
        //Set new pause state
        isInPause = !isInPause;
    }
}
