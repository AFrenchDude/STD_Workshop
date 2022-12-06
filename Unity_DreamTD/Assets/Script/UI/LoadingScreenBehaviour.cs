using UnityEngine;

//Made by Melinon Remy
public class LoadingScreenBehaviour : MonoBehaviour
{
    //Destroy loading screen once scene loaded and appearing anim is over
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
