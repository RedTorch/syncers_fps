using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor_Net : MonoBehaviour
{
    private NetworkCharacterControllerPrototype controller;
    private Vector3 playerVelocity;
    public float speed = 5f;

    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<NetworkCharacterControllerPrototype>();
        if (controller == null)
        {
            Debug.LogError("No NetworkCharacterControllerPrototype component found.");  
        }
    }

    // Update is called once per frame
    void Update()
    {
        // nothing here!
    }

    public void ProcessMove(Vector2 input) 
    {
        Vector3 moveDirection = new Vector3(0,0,0);
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); //error
        
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0) {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        controller.Jump();
    }
}
