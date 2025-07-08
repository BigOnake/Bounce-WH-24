using UnityEngine;

public class P_GroundCheck_Network : MonoBehaviour
{
    public P_Movement_Network playerController;

    private void OnTriggerEnter(Collider other)
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
