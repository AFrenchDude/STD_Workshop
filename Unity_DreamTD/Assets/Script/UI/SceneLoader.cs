using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    public void SceneLoad()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void ReloadScene()
    {
        string thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene);
    }
}
