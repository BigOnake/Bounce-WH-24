using UnityEngine;

public class animationStateController : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement playerMovement;
    float zVel, xVel, yVel;

    void Start()
    {
        Debug.Log(animator);
    }

    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool isJumping = animator.GetBool("isJumping");
        bool isPunching = animator.GetBool("isPunching");
        bool isIdle = animator.GetBool("isIdle");

        if (!isRunning && isHorizontalMove())
        {
            animator.SetBool("isRunning", true);
        }
        if(isRunning && !isHorizontalMove())
        {
            animator.SetBool("isRunning", false);
        }

        if(!isJumping && !isOnGround())
        {
            animator.SetBool("isJumping", true);
        }
        if (isJumping && isOnGround())
        {
            animator.SetBool("isJumping", false);
        }
    }

    private bool isHorizontalMove()
    {
        return (playerMovement.wishDir.z != 0) || (playerMovement.wishDir.x != 0);
    }

    private bool isOnGround()
    {
        return playerMovement.GetGrounded();
    }
}
