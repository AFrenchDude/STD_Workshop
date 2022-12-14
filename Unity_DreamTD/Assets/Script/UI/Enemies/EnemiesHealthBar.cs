// Made by Dorian Alexandre & Albert Esteban
using UnityEngine;
using UnityEngine.UI;


public class EnemiesHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    private PathFollower _pathFollower;


    private void OnEnable()
    {
        _pathFollower?.LastWaypointReached.RemoveListener(SelfDestroy);
        _pathFollower?.LastWaypointReached.AddListener(SelfDestroy);
    }

    private void OnDisable()
    {
        _pathFollower.LastWaypointReached.RemoveListener(SelfDestroy);
    }

    public void SetPathFollower(PathFollower pathFollower)
    {
        _pathFollower = pathFollower;

        _pathFollower.LastWaypointReached.RemoveListener(SelfDestroy);
        _pathFollower.LastWaypointReached.AddListener(SelfDestroy);
    }

    public void SelfDestroy(PathFollower pathFollower)
    {
        Destroy(gameObject);
    }

    public void UpdateLife(float life, float maxLife)
    {
        _slider.value = life / maxLife;
    }
}
