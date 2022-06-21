using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

public class InputManager_Net : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor_Net motor;
    private PlayerLook_Net look;

    private Vector2 lookData;
    private Vector2 moveData;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor_Net>();
        look = GetComponent<PlayerLook_Net>();

        lookData = Vector3.zero;
        moveData = Vector3.zero;

        // onFoot.Jump.performed += (ctx => motor.Jump());
    }

    // Update is called once per frame
    void Update()
    {
        // motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        // look.ProcessLook(onFoot.Look.ReadValue<Vector2>());

        // tell the PlayerMotor to move using the value from our movement action
        motor.ProcessMove(moveData);
        look.ProcessLook(lookData);
        moveData = Vector2.zero;
        lookData = Vector2.zero;
    }

    public void setLookData(Vector2 iLookData)
    {
        lookData = iLookData;
    }

    public void setMoveData(Vector2 iMoveData)
    {
        moveData = iMoveData;
    }

    // BELOW IS IN PROGRESS
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            //_cc.Move(5*data.direction*Runner.DeltaTime);
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        }
    }

    private void LateUpdate() 
    {
        // look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
}
