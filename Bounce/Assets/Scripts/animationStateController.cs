using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool forwardPressed = Input.GetKey("w");
        // if player presses w key
        if (!isRunning && forwardPressed)
        {
            // then set the isRunning boolean to true
            animator.SetBool("isRunning", true);
        }
        // if player not pressing w key
        if (isRunning && !forwardPressed)
        {
            // then set the isRunning boolean to be false
            animator.SetBool("isRunning", false);
        }
    }
}
