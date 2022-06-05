using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    float x, z;
    float speed = 0.1f;

    public GameObject cam;
    Quaternion cameraRotation, charRotation;
    float Xsensityvity = 3f, Ysensityvity = 3f;
    
    bool cursorLock = true;

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

    float jumpForce = 300.0f;
    public bool isJumping = false;


    // Start is called before the first frame update
    void Start()
    {
        cameraRotation = cam.transform.localRotation;
        charRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float xRotation = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRotation = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRotation *= Quaternion.Euler(-yRotation, 0, 0);
        charRotation *= Quaternion.Euler(0, xRotation, 0);

        //Updateの中で作成した関数を呼ぶ
        cameraRotation = ClampRotation(cameraRotation);

        cam.transform.localRotation = cameraRotation;
        transform.localRotation = charRotation;

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false) {
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
            isJumping = true;
        }

        UpdateCursorLock();
    }

    private void FixedUpdate()
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;

        transform.position += cam.transform.forward * z + cam.transform.right * x;
    }


    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        } else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }


        if (cursorLock) 
        {
            Cursor.lockState = CursorLockMode.Locked;
        } else if (!cursorLock) 
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）: wはスカラー（座標とは無関係の量）)

        q.w = 1f;
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;
        angleX = Mathf.Clamp(angleX, minX, maxX);
        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("floor")) {
            isJumping = false;
        }
    }

}