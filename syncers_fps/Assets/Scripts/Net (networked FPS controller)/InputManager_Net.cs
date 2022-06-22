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

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {

        // tell the PlayerMotor to move using the value from our movement action
        if (GetInput(out NetworkInputData_Net data))
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
            if(data.fire)
            {
                // fire weapon! account for cooldown
                print("firing...");
            }
            if(data.reload)
            {
                // reloads current weapon (assuming it's a normal magazine-based gun; if it recharges over time, this is not needed)
            }
            if(data.ability1)
            {
                // uses ability #1; add ability2, ability3, etc.. as needed
            }
        }
        else
        {
            print("GetInput not executed");
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
