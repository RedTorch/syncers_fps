using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    float playerSpeed = 2.5f;

    float jumpForce = 200.0f;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         // left
        if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        }
        // right
        if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        }
        // forward
        if (Input.GetKey(KeyCode.W)) {
            transform.position += Vector3.forward * playerSpeed * Time.deltaTime;
        }
        // back
        if (Input.GetKey(KeyCode.S)) {
            transform.position += Vector3.back * playerSpeed * Time.deltaTime;
        }

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false) {
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("floor")) {
            isJumping = false;
        }
    }
}
