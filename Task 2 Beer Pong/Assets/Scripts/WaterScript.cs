using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{

    public PhysicMaterial ballPhysicMaterial;
    public PhysicMaterial wetBallPhysicMaterial;
    public float reducedBounciness = 0.6f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            SphereCollider ball = other.gameObject.GetComponent<SphereCollider>();
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            ball.material = wetBallPhysicMaterial;
            rb.velocity *= reducedBounciness;
        }
    }
}
