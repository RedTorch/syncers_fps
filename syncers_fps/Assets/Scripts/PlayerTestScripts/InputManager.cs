using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;


    private PlayerMotor_Net motor;
    private PlayerLook_Net look;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor_Net>();
        look = GetComponent<PlayerLook_Net>();

        onFoot.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    void Update()
    {
        // tell the PlayerMotor to move using the value from our movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate() 
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable() 
    {
        onFoot.Disable();
    }
}
