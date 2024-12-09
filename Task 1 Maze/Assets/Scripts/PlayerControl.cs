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

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        Quaternion targetRotation = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        float angle = Mathf.Sin(Time.time * armSwingSpeed) * armSwingAngle;
        leftArm.localRotation = Quaternion.Euler(angle, 0, 0);
        rightArm.localRotation = Quaternion.Euler(-angle, 0, 0);
    }
}