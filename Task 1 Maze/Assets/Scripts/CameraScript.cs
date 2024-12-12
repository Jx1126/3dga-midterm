using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject playerObject;
    public Vector3 cameraOffset;
    void Start()
    {
    }

    void LateUpdate()
    {
        transform.position = playerObject.transform.position + playerObject.transform.rotation * cameraOffset;
        transform.rotation = Quaternion.Euler(20, playerObject.transform.rotation.eulerAngles.y, 0);
    }
}