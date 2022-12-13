//By ALBERT Esteban
using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{
    [Header("Can be ignore if called through script")]
    [SerializeField] LevelSave _levelSaveToUnlock = null;
    public void UnlockLevel()
    {
        _levelSaveToUnlock.SetIsLock(false);
    }
    public void UnlockLevel(LevelSave levelSave)
    {
        levelSave.SetIsLock(false);
    }
}
