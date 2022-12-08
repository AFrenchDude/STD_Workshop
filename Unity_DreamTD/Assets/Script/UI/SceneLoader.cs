using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Made by Melinon Remy
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;

    //Ref to new scene to load
    [SerializeField] private string sceneToLoad;
    //Load new scene
    public void SceneLoad()
    {
        if(loadingScreen != null)
        {
            StartCoroutine(LoadSceneCoroutine(sceneToLoad));
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    //Reload current scene
    public void ReloadScene()
    {
        //Get current scene
        string thisScene = SceneManager.GetActiveScene().name;
        if (loadingScreen != null)
        {
            StartCoroutine(LoadSceneCoroutine(thisScene));
        }
        else
        {
            SceneManager.LoadScene(thisScene);
        }
    }

    // Loads a scene asynchronously and display loading screen
    private IEnumerator LoadSceneCoroutine(string scene)
    {
        // Making the loading screen appear
        var loadingScreenInstance = Instantiate(loadingScreen);
        // Making the loading screen persistent after we unloaded the scene
        DontDestroyOnLoad(loadingScreenInstance);
        //wait for animation
        yield return new WaitForSecondsRealtime(0.15f);

        // Start loading the scene in the background
        var loading = SceneManager.LoadSceneAsync(scene);
        // Disable auto-load 
        loading.allowSceneActivation = false;

        // Getting the loading screen animator
        var loadingAnimator = loadingScreenInstance.GetComponent<Animator>();
        // Getting current animator state to retrieve the length in seconds of the actual animation
        var currentAnimTime = loadingAnimator.GetCurrentAnimatorStateInfo(0).length;

        // While the scene is still loading
        while (!loading.isDone)
        {
            // If the scene loaded at 90% (which means 100% in Unity)
            if (loading.progress >= 0.9f)
            {
                // Make the new scene visible
                loading.allowSceneActivation = true;
                if (currentAnimTime > 2)
                {
                    // Wait for the end of the appearing animation before switching scenes
                    yield return new WaitForSecondsRealtime(currentAnimTime);
                    // Launch the disappear animation
                    loadingAnimator.SetTrigger("Disapear");
                }
                else
                {
                    yield return new WaitForSecondsRealtime(2);
                    // Launch the disappear animation
                    loadingAnimator.SetTrigger("Disapear");
                }
            }
        }
    }
}
