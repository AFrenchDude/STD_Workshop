using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Made by Melinon Remy
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    //Ref to new scene to load
    private string sceneToLoad;


    public void SetUpScene(string levelName)
    {
        sceneToLoad = levelName;
    }
    //Load new scene
    public void SceneLoad()
    {
        Debug.Log(sceneToLoad);
        if(Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            if (loadingScreen != null)
            {
                StartCoroutine(LoadSceneCoroutine(sceneToLoad));
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
    //Reload current scene
    public void ReloadScene()
    {
        Time.timeScale = 1.0f;
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
        Time.timeScale = 1.0f;
        // Making the loading screen appear
        var loadingScreenInstance = Instantiate(loadingScreen);
        // Making the loading screen persistent after we unloaded the scene
        DontDestroyOnLoad(loadingScreenInstance);
        //wait for animation
        yield return new WaitForSecondsRealtime(0.3f);

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
                //Debug wait for not seeing old scene
                yield return new WaitForSecondsRealtime(0.1f);
                // Make the new scene visible
                loading.allowSceneActivation = true;
                // Launch the disappear animation
                loadingAnimator.SetTrigger("Disapear");
                // Wait for the end of the appearing animation before showing scene
                yield return new WaitForSecondsRealtime(currentAnimTime);
            }
        }
    }
}
