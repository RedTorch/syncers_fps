using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingOriginCube : MonoBehaviour
{
    // Start is called before the first frame update
    public float destroyTime = 4f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        // placeholder
    }
}
