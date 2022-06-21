using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerMotor_Net : MonoBehaviour
{
    private NetworkCharacterControllerPrototype controller;
    private Vector3 playerVelocity;
    public float speed = 5f;

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
        // controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);  //error
        controller.Move(transform.TransformDirection(new Vector3(input.x,0,input.y)));
        
        // playerVelocity.y += gravity * Time.deltaTime;
        // if (isGrounded && playerVelocity.y < 0) {
        //     playerVelocity.y = -2f;
        // }
        // controller.Move(playerVelocity * Time.deltaTime); //error
        // Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        controller.Jump();
    }
}
