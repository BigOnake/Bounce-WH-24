using Unity.Netcode;
using UnityEngine;

public class PlayerDeathDetection_Network : NetworkBehaviour
{
    public static event System.Action onPlayerHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsOwner)
            return;

        if (!(collision.gameObject.TryGetComponent<NetworkObject>(out NetworkObject hitNetObj)))
            return;

        CheckHitboxForBall(ref collision);
    }

    private void CheckHitboxForBall(ref Collision col)
    {
        if (col.gameObject.tag != "Ball")
            return;

        Debug.Log($"{col.gameObject.name} was hit");
        onPlayerHit.Invoke();
    }
}
