using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private float attackDuration = 0.1f;
    [SerializeField] private Cooldown attackCooldown;
    [SerializeField] private Transform cameraTransform;

    private bool isAttacking = false;

    void Start()
    {
        attackHitbox.SetActive(false);
    }

    public void PerformAttack()
    {
        if (!isAttacking && !attackCooldown.IsOnCooldown())
        {
            Debug.Log("Attack Started..."); //DELETE
            isAttacking = true;
            attackHitbox.SetActive(true);
            attackCooldown.StartCooldown();

            Vector3 lookDirection = cameraTransform.forward;
            attackHitbox.transform.rotation = Quaternion.LookRotation(lookDirection);
            attackHitbox.transform.position = transform.position + lookDirection * 1.5f;

            Invoke(nameof(EndAttack), attackDuration); //maybe replace with end of animation which calls end attack
        }
    }

    private void EndAttack()
    {
        Debug.Log("Attack Ended."); //DELETE
        attackHitbox.SetActive(false);
        isAttacking = false;
    }

    public bool IsAttacking() { return isAttacking; }
}
