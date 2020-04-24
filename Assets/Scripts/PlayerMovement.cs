using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 veloctiy;
    bool isGrounded;
  
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && veloctiy.y <0)
        {
            veloctiy.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;//controls the local direction of the player to move and rotate
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            veloctiy.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        veloctiy.y += gravity * Time.deltaTime;//creating gravity

        controller.Move(veloctiy * Time.deltaTime);// the equation is time squared

    }
}
