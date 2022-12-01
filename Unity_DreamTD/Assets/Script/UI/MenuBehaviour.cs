using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SceneLoader(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
