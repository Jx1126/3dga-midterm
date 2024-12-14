using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public Transform leftArm;
    public Transform rightArm;
    public float movementSpeed = 15f;
    public float cameraSensitivity = 3f;    
    public TMP_Text keyStatusText;

    private Rigidbody rb;
    private Vector3 movement;
    private CameraScript cameraScript;
    private float armSwingSpeed = 10f;
    private float armSwingAngle = 30f;
    private bool collectedKey = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraScript = FindObjectOfType<CameraScript>();

        // Set the initial key status to unobtained
        keyStatusText.text = "Key: Unobtained";
        keyStatusText.color = Color.red;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0)
        {
            // Update the player rotation based on the camera rotation
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, cameraScript.transform.eulerAngles.y + x * cameraSensitivity, transform.eulerAngles.z);
        }

        // Check if the player is moving
        if(x != 0 || y != 0)
        {
            // Swing the arms when the player is moving
            float angle = Mathf.Sin(Time.time * armSwingSpeed) * armSwingAngle;
            leftArm.localRotation = Quaternion.Euler(angle, 0, 0);
            rightArm.localRotation = Quaternion.Euler(-angle, 0, 0);
        }
        else
        {
            // Reset the arms to their default position when the player is not moving
            leftArm.localRotation = Quaternion.Euler(0, 0, 0);
            rightArm.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        float y = Input.GetAxis("Vertical");
        if (y != 0)
        {
            // Move the player based on the input
            rb.velocity = this.transform.forward * y * movementSpeed;
        }
        else
        {
            // Stop the player if there is no input
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Key"))
        {
            // Destroy the key object and update the key status when a object with the tag "Key" is collected
            Destroy(other.gameObject);
            collectedKey = true;
            keyStatusText.text = "Key: Obtained";
            keyStatusText.color = Color.green;
        }

        if(other.gameObject.CompareTag("Exit") && collectedKey)
        {
            // Destroy the exit barrier and load the end scene when the player reaches the exit with the key
            Destroy(other.gameObject);
            SceneManager.LoadScene("EndScene");
        }
    }
}