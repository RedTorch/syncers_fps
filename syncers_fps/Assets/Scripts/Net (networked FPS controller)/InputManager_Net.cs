using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

public class InputManager_Net : NetworkBehaviour
{
    private PlayerMotor_Net motor;
    private PlayerLook_Net look;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        motor = GetComponent<PlayerMotor_Net>();
        look = GetComponent<PlayerLook_Net>();

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
            motor.ProcessMove(data.move);
            look.ProcessLook(data.look);
        }
    }

    // BELOW IS IN PROGRESS
    // public override void FixedUpdateNetwork()
    // {
    //     if (GetInput(out NetworkInputData data))
    //     {
    //         data.direction.Normalize();
    //         //_cc.Move(5*data.direction*Runner.DeltaTime);
    //         motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    //         look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    //     }
    // }

    private void LateUpdate() 
    {
        // look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
}
