using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [Header("Particle Prefabs")]
    [SerializeField] private ParticleSystem landingParticles;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private GameObject deathParticlesPrefab; //remove in future

    public void PlayLandingParticles(Component sender, object id)
    {
        if (transform.GetComponentInParent<PlayerId>().GetId() != (int)id) { return; }

        landingParticles.Play();
    }

    public void PlayDeathParticles(Component sender, object id)
    {
        if (transform.GetComponentInParent<PlayerId>().GetId() != (int)id) { return; }

        //deathParticles.Play();
        GameObject particles = Instantiate(deathParticlesPrefab, deathParticles.transform.position, Quaternion.identity);
    }
}
