using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

public class PlayerInput_Net : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    public Vector2 move;
    public Vector2 look;

    public object controlledObject;

    private void Awake() {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        print("PlayerInput_net is awake");
    }

    public void Update() {
        move = onFoot.Movement.ReadValue<Vector2>();
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