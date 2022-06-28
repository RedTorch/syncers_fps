using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// an input receiver class attached to the basic spawner object in the scene
// it is used by BasicSpawner_Net which reads the data in OnInput()
// we could probably combine this script with BasicSpawner entirely, but we separate these scripts to keep things clean.

// these docs are useful reference: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.PlayerInput.html

public class PlayerInput_Net : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool jump2;
    public bool fire;
    public bool reload;
    public bool ability1;

    public object controlledObject;

    private void Awake() {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
    }

    public void Update() {
        move = onFoot.Movement.ReadValue<Vector2>();
        look = onFoot.Look.ReadValue<Vector2>();
        jump = onFoot.Jump.triggered;
        jump2 = (float)(onFoot.Jump2.ReadValue<float>()) > 0.1f;
        print(onFoot.Jump2.ReadValue<float>());
        print(jump2);
        fire = onFoot.Fire.ReadValue<float>() > 0.1f;
        reload = onFoot.Reload.triggered;
        ability1 = onFoot.Ability1.triggered;
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