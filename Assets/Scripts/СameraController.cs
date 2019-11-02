using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СameraController : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = player.position.x + 6;
        position.z = player.position.z + 10;
        transform.position = position;
    }
}
