using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// an input receiver class attached to the basic spawner object in the scene
// it is used by BasicSpawner_Net which reads the data in OnInput()
// we could probably combine this script with BasicSpawner entirely, but for organization we separate these scripts.

public class PlayerInput_Net : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    public Vector2 move;
    public Vector2 look;
    public bool isJumping;

    public object controlledObject;

    private void Awake() {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
    }

    public void Update() {
        move = onFoot.Movement.ReadValue<Vector2>();
        look = onFoot.Look.ReadValue<Vector2>();
        isJumping = onFoot.Jump.triggered;
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