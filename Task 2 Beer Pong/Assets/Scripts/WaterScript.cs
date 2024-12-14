using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public PhysicMaterial ballPhysicMaterial;
    public PhysicMaterial wetBallPhysicMaterial;
    public float reducedBounciness = 0.2f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            SphereCollider ball = other.gameObject.GetComponent<SphereCollider>();
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            ball.material = wetBallPhysicMaterial;
            rb.velocity *= reducedBounciness;

            BallScript ballScript = other.gameObject.GetComponent<BallScript>();
            if (ballScript != null)
            {
                ballScript.ballShot = true;
            }
            
            Destroy(other.gameObject, 2.0f);
            ScoreScript.instance.AddScore();
        }
    }
}
