using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float sumRotation;
    static bool isActive = true;
    public bool flipRotation = false;
    public float rotationSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        sumRotation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
            sumRotation += Time.deltaTime * rotationSpeed * (flipRotation ? -1 : 1);
            transform.rotation = Quaternion.Euler(0, sumRotation, 0);
        }
    }

    public static void Stop() {
        isActive = false;
    }
}
