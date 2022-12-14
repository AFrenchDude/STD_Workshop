using System.Collections.Generic;
using UnityEngine;

//Made by Alexandre Dorian
public class ParticleActivation : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> _particles;

    public void ActiveParticle(int index)
    {
        if(_particles.Count > index)
        {
            if (_particles[index] != null)
            {
                _particles[index].Clear();
                _particles[index].Play();
            }
        }
    }
}
