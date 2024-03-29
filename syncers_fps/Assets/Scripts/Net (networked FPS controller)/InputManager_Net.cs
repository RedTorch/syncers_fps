// Reference:
// https://doc.photonengine.com/en-us/fusion/current/fusion-100/fusion-103
// https://doc-api.photonengine.com/en/fusion/current/class_fusion_1_1_network_behaviour.html#a1d45dad0ae1a816ae0aa02db93aa2457
// PUN 2: https://doc.photonengine.com/en-us/pun/current/demos-and-tutorials/pun-basics-tutorial/player-networking



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

public class InputManager_Net : NetworkBehaviour
{

    private NetworkCharacterControllerPrototypeCustom controller;

    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public PlayerRef playerReference;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;

        controller = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        if (controller == null)
        {
            Debug.LogError("No NetworkCharacterControllerPrototype component found.");  
        }

        // onFoot.Jump.performed += (ctx => motor.Jump());
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData_Net data))
        {
            // if(data.callingPlayer != playerReference) MoveChar(data);
            MoveChar(data);
        }
    }

    public void MoveChar(NetworkInputData_Net data)
    {
        Vector2 input = data.move;
        Vector3 moveVector = transform.forward*input.y + transform.right*input.x;
        moveVector.Normalize();
        controller.Move(moveVector);
        ProcessLook(data.look);
        if(data.jump)
        {
            controller.Jump();
        }
        if(data.jump2 == false)
        {
            controller.isFloatJump = false;
        }
        if(data.fire)
        {
            // fire weapon! account for cooldown
            print("firing...");
        }
        if(data.reload)
        {
            // reloads current weapon (assuming it's a normal magazine-based gun; if it recharges over time, this is not needed)
            print("reloading!");
        }
        if(data.ability1)
        {
            // uses ability #1; add ability2, ability3, etc.. as needed
            print("ability1 used!");
        }
    }

    public void ProcessLook(Vector2 input) 
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //calculate camera rotation for looking up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        // apply the above to camera transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // rotate player to look left and right
        // transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
        controller.Rotate((mouseX * Time.deltaTime) * xSensitivity);
    }

    private void LateUpdate() 
    {
        // look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
}
