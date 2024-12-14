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
        // Check if the object that touched the water is a ball
        if (other.gameObject.tag == "Ball")
        {
            SphereCollider ball = other.gameObject.GetComponent<SphereCollider>();
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            // Change the ball's physic material so it bounces less after touching the water
            ball.material = wetBallPhysicMaterial;
            // Reduce the ball's velocity to reduce its speed
            rb.velocity *= reducedBounciness;

            BallScript ballScript = other.gameObject.GetComponent<BallScript>();
            if (ballScript != null)
            {
                // Set the ballShot to true so the player can spawn the next ball even if it's destroyed
                ballScript.ballShot = true;
            }

            // Destroy the ball after 2 seconds
            Destroy(other.gameObject, 2.0f);
            // Add score when the ball touches the water
            ScoreScript.instance.AddScore();
        }
    }
}
