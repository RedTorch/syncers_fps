using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// an input receiver class attached to the basic spawner object in the scene
// it is used by the 

public class PlayerInput_Net : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    public Vector2 move;
    public Vector2 look;

    public object controlledObject;

    private void Awake() { //running as expected
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
    }

    public void Update() { //move and look are being set properly
        move = onFoot.Movement.ReadValue<Vector2>();  //error
        look = onFoot.Look.ReadValue<Vector2>();
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