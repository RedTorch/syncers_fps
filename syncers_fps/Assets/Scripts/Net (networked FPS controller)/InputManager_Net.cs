using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

public class InputManager_Net : NetworkBehaviour
{
    private PlayerMotor_Net motor;
    private PlayerLook_Net look;

    private NetworkCharacterControllerPrototype controller;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        motor = GetComponent<PlayerMotor_Net>();
        look = GetComponent<PlayerLook_Net>();

        controller = GetComponent<NetworkCharacterControllerPrototype>();
        if (controller == null)
        {
            Debug.LogError("No NetworkCharacterControllerPrototype component found.");  
        }

        // onFoot.Jump.performed += (ctx => motor.Jump());
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        // motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        // look.ProcessLook(onFoot.Look.ReadValue<Vector2>());

        // tell the PlayerMotor to move using the value from our movement action
        if (GetInput(out NetworkInputData_Net data))
        {
            Vector2 input = data.move.normalized;
            if(input.magnitude != 0)
            {
                controller.Move(transform.TransformDirection(new Vector3(input.x,0,input.y))*1f);
            }
            look.ProcessLook(data.look);
        }
        else
        {
            print("GetInput not executed");
        }
    }

    private void LateUpdate() 
    {
        // look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
}
