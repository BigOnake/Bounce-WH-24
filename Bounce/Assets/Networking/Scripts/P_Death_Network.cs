using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Death_Network : NetworkBehaviour
{
    /*
     * TODO: Despawn the player
     * Play death animation / particles
     *
     */
    [SerializeField] private SkinnedMeshRenderer model;
    private CapsuleCollider bodyCollider;
    private Rigidbody playerRb;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private AudioClip c_death;
    [SerializeField] private AudioSource s_death;

    private PlayerInput p_input;
    private NetInputController np_input;
    public static event System.Action onPlayerHit;
    private bool isDead = false;

    private void Awake()
    {
        p_input = GetComponent<PlayerInput>();
        np_input = GetComponent<NetInputController>();
        playerRb = GetComponent<Rigidbody>();
        bodyCollider = GetComponent<CapsuleCollider>();
    }

    public void Die()
    {
        DisableInputs();
        PlayDeathRpc();
    }

    private void DisableInputs()
    {
        Debug.Log("Disabling inputs");
        np_input.enabled = false;
        p_input.enabled = false;
    }

    private void PlayDeathAnimation()
    {
        if (deathParticles)
            deathParticles.Play();
    }

    private void PlayDeathSound()
    {
        if (c_death)
            s_death.PlayOneShot(c_death);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void PlayDeathRpc()
    {
        PlayDeathAnimation();
        PlayDeathSound();
        DisableVisuals();
    }

    private void DisableVisuals()
    {
        model.enabled = false;
        playerRb.useGravity = false;
        playerRb.isKinematic = true;
        bodyCollider.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsOwner || isDead)
            return;

        if (!(collision.gameObject.GetComponent<NetworkObject>()))
            return;

        Debug.Log("Its the owner!");

        CheckHitboxForBall(ref collision);
    }

    private void CheckHitboxForBall(ref Collision col)
    {
        if (col.gameObject.tag != "Ball")
            return;

        Debug.Log($"{col.gameObject.name} was hit");
        Die();
        onPlayerHit?.Invoke();
        isDead = true;
    }
}
