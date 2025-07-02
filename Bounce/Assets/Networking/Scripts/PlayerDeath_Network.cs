using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeath_Network : NetworkBehaviour
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
        model.enabled = false;
        bodyCollider.enabled = false;
        playerRb.useGravity = false;
        playerRb.isKinematic = true;
    }

    private void Despawn()
    {
        //Invoke event and send it to PlayersManager_Network
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsOwner || isDead)
            return;

        if (!(collision.gameObject.TryGetComponent<NetworkObject>(out NetworkObject hitNetObj)))
            return;

        Debug.Log("Its the owner!");

        CheckHitboxForBall(ref collision, ref hitNetObj);
    }

    private void CheckHitboxForBall(ref Collision col, ref NetworkObject hitNetObj)
    {
        if (col.gameObject.tag != "Ball")
            return;

        Debug.Log($"{col.gameObject.name} was hit");
        Die();
        onPlayerHit?.Invoke();
        isDead = true;
    }
}
