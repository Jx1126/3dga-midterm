using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 cameraOffset;
    void Start()
    {
    }

    void LateUpdate()
    {
        transform.position = targetObject.transform.position + targetObject.transform.rotation * cameraOffset;
        transform.rotation = Quaternion.Euler(30, targetObject.transform.rotation.eulerAngles.y, 0);
    }
}