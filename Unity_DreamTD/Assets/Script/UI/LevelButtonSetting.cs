using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Melinon Remy
public class LevelButtonSetting : MonoBehaviour
{
    //Level source
    [SerializeField] private LevelSave levelSave;

    //Display save on
    [SerializeField] private Image imageLevel;
    [SerializeField] private GameObject imagesStars;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    [SerializeField] GameObject _lockContainer;

    //Star images ref
    [SerializeField] private Color _emptyMoonColor;
    [SerializeField] private Color _fullMoonColor;

    //
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartLevel()
    {
        SceneLoader sceneLoader = GetComponent<SceneLoader>();

        if(levelSave.IsLock == false)
        {
            sceneLoader.SetUpScene(levelSave.LevelName);
            sceneLoader.SceneLoad();
        }
    }

    public void Hover()
    {
        if (levelSave.IsLock == false)
        {
            _animator.SetBool("Unlock", true);          
        }
        _animator.SetBool("Overlap",true);
    }
    public void Unhover()
    {
        _animator.SetBool("Overlap", false);
    }


    //Set
    void Start()
    {
        bestScoreText.SetText(levelSave.BestScore.ToString());
        imageLevel.sprite = levelSave.LevelImage;
        for (int i = 0; i != imagesStars.transform.childCount; i++)
        {
            if (levelSave.StarNumber > i && imagesStars.transform.GetChild(i).GetComponent<Image>() != null)
            {
                imagesStars.transform.GetChild(i).GetComponent<Image>().color = _fullMoonColor;
            }
            else
            {
                imagesStars.transform.GetChild(i).GetComponent<Image>().color = _emptyMoonColor;
            }
        }

        

        if (levelSave.IsLock == false)
        {
            _animator.SetBool("Unlock", true);
            Destroy(_lockContainer);

        }
        else
        {
            _lockContainer.gameObject.SetActive(false);
            _animator.SetBool("Unlock", false);
        }
    }
}
