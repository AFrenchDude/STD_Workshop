// CE FICHIER A ETE CODE PAR L4INCROYABLE DORIAN ALEXANDRE (le grand)
// Enfait, Esteban l'a aidé : lol
// C'est vrai ?
// Oui...
// Bon et bien on kill le projet...

using System.Collections;
using System.Collections.Generic;
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
