using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            BallPhysics ball = other.GetComponent<BallPhysics>();
            if (ball != null)
            {
                ball.HitByPlayer(transform.forward);
                gameObject.SetActive(false);
            }
        }
    }
}
