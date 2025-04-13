using UnityEngine;

public class animationStateController : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement playerMovement;
    float zVel, xVel, yVel;
    bool isRunning;
    bool isJumping;
    bool isPunching;
    bool isIdle;

    void Start()
    {
        Debug.Log(animator);
    }

    void Update()
    {
        isRunning = animator.GetBool("isRunning");
        isJumping = animator.GetBool("isJumping");
        isPunching = animator.GetBool("isPunching");

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

        /*if(isPunching)
        {
            animator.SetTrigger("isPunching");
        }*/
    }

    private bool isHorizontalMove()
    {
        return (playerMovement.wishDir.z != 0) || (playerMovement.wishDir.x != 0);
    }

    private bool isOnGround()
    {
        return playerMovement.GetGrounded();
    }

    public void OnAttack()
    {
        //isPunching = true;
    }
}
