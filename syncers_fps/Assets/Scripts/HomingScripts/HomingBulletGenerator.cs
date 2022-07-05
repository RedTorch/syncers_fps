using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletGenerator : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    GameObject missile;

    [SerializeField]
    GameObject cube;

    [SerializeField, Min(1)]
    int iterationCount = 3;

    [SerializeField]
    float interval = 0.1f;

    bool isSpawning = false;
    Transform thisTransform;
    WaitForSeconds intervalWait;

    void Start()
    {
        thisTransform = transform;
        intervalWait = new WaitForSeconds(interval);
    }

    void Update()
    {
        if (isSpawning)
        {
            return;
        }
        StartCoroutine(nameof(SpawnMissile));
    }

    IEnumerator SpawnMissile()
    {
        isSpawning = true;

        Vector3 euler;
        Quaternion rot;
        HomingScript homing;

        // for (int i = 0; i < iterationCount; i++){
        //     homing = Instantiate(missile, thisTransform.position, Quaternion.identity).GetComponent<HomingTest>();
        //     Instantiate(cube, thisTransform.position, thisTransform.rotation);
        //     homing.Target = target;
        // }

        SpawnMissileSingle();

        yield return intervalWait;
        isSpawning = false;
    }

    void SpawnMissileSingle(float xOffset = 0f, float yOffset = 0f, float zOffset = 0f)
    {
        HomingScript homing = Instantiate(missile, thisTransform.position + transform.right*xOffset + transform.up*yOffset + transform.forward*zOffset, Quaternion.identity).GetComponent<HomingScript>();
        Instantiate(cube, thisTransform.position, Quaternion.identity);
        homing.Target = target;
    }
}