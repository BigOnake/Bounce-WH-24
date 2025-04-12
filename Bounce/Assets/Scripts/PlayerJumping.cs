using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    public Rigidbody playerRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void Jump()
    {
        playerRb.AddForce(transform.up * 1000);
    }
}
