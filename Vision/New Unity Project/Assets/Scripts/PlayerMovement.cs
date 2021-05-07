using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;

    // Variables
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    
    private Vector3 velocity;
    private bool isGrounded;
    
    // Grounded
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    void Update()
    {
        // Check sphere
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        // Grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        Controller.Move(move * speed * Time.deltaTime);
        
        //Gravity
        velocity.y += gravity * Time.deltaTime;
        Controller.Move(velocity * Time.deltaTime);
        
        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
