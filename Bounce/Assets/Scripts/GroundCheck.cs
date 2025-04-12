using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerController.SetGrounded(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerController.SetGrounded(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerController.SetGrounded(true);
        }
    }
}
