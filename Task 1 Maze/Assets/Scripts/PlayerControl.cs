using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public Transform leftArm;
    public Transform rightArm;
    public float speed = 15f;
    public float armSwingSpeed = 10f;
    public float armSwingAngle = 30f;

    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical).normalized;

        if (movement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

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

    void FixedUpdate()
    {
        if (movement.magnitude > 0)
        {
            rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}