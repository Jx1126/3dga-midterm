using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform leftArm;
    public Transform rightArm;
    public float speed = -15f;
    public float armSwingSpeed = 10f;
    public float armSwingAngle = 30f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Vector3 movement;
    private CameraScript cameraScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraScript = FindObjectOfType<CameraScript>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, cameraScript.transform.eulerAngles.y + x * rotationSpeed, transform.eulerAngles.z);
            
        }
        if(x != 0 || y != 0)
        {
            float angle = Mathf.Sin(Time.time * armSwingSpeed) * armSwingAngle;
            leftArm.localRotation = Quaternion.Euler(angle, 0, 0);
            rightArm.localRotation = Quaternion.Euler(-angle, 0, 0);
        }
        else
        {
            leftArm.localRotation = Quaternion.Euler(0, 0, 0);
            rightArm.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void FixedUpdate() {
        float y = Input.GetAxis("Vertical");
        if (y != 0)
        {
            rb.velocity = this.transform.forward * y * speed;
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}