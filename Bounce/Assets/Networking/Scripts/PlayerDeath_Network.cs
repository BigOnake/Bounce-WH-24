using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeath_Network : MonoBehaviour
{
    /*
     * TODO: Despawn the player
     * Play death animation / particles
     *
     */

    [SerializeField] private ParticleSystem deathParticles;

    [SerializeField] private AudioClip c_death;
    [SerializeField] private AudioSource s_death;
    private PlayerInput p_input;

    private void Awake()
    {
        p_input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        PlayerDeathDetection_Network.onPlayerHit += Die;
    }

    private void OnDisable()
    {
        PlayerDeathDetection_Network.onPlayerHit -= Die;
    }

    public void Die()
    {
        p_input.enabled = false;
        PlayDeathAnimation();
        PlayDeathSound();
    }

    public void DeathFinished()
    {
        //playerDeathFinishedEvent.Raise(this, transform.GetComponentInParent<PlayerId>().GetId());
    }

    private void PlayDeathAnimation()
    {
        if(deathParticles)
            Instantiate(deathParticles, deathParticles.transform.position, Quaternion.identity);
    }

    private void PlayDeathSound()
    {
        if (c_death)
            s_death.PlayOneShot(c_death);
    }

    private void Despawn()
    {
        //Invoke event and send it to PlayersManager_Network
    }
}
