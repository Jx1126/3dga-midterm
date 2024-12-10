using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public float keyRotationSpeed = 100f; 
    public float keyFloatingHeight = 0.3f;
    public float keyFloatingSpeed = 3f;
    private Vector3 originalPosition;
    

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, keyRotationSpeed * Time.deltaTime);
        
        float floatingPosition = Mathf.Sin(Time.time * keyFloatingSpeed) * keyFloatingHeight;
        transform.position = originalPosition + new Vector3(0, floatingPosition, 0);
    }
}
