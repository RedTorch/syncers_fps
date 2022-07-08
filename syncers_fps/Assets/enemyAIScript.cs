using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAIScript : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
        if(Vector3.Distance(transform.position, player.transform.position) > 15f)
        {
            transform.Translate(Vector3.Normalize(player.transform.position - transform.position)*0.5f);
        }
        // else
        // {
        //     transform.Translate(transform.right*0.2f);
        // }
    }
}