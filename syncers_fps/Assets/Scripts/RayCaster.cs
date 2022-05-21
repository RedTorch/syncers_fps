using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
    }

    void Shot() 
    {
        int distance = 100;
        Vector3 center = new Vector3(Screen.width/2, Screen.height/2, 0);
        Ray ray = playerCamera.ScreenPointToRay(center);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            Debug.Log("オブジェクトにぶつかりました。");
            Destroy(hitInfo.collider.gameObject);
        }
    }
}
