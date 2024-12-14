using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    public float forceMultiplier = 10.0f;
    public float maxForce = 50.0f;
    public bool ballShot = false;

    private Rigidbody rb;
    private Vector3 previousMousePos;
    private Vector3 mouseDragPos;
    private bool mouseDragging = false;

    private Camera mainCamera;
    private Image forceBar;
    private RectTransform forceBarContainer;

    void Start()
    {
        // Set the ball's rigidbody to kinematic so it doesn't get affected by physics until it's shot
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mouseDragging)
        {
            Vector3 mousePos = GetMousePos();
            previousMousePos = Input.mousePosition;

            // Calculate the force based on the distance between the mouse initial position and the current position
            Vector3 direction = mouseDragPos - mousePos;
            float magnitude = Mathf.Min(direction.magnitude * forceMultiplier, maxForce);

            // Update the force bar fill amount based on the force
            forceBar.fillAmount = magnitude / maxForce;
        }
    }

    void OnMouseDown()
    {
        if (!mouseDragging)
        {
            mouseDragging = true;
            // Make the ball kinematic so it doesn't get affect by physics before it's shot
            rb.isKinematic = true;
            mouseDragPos = GetMousePos();
            // Display the force bar when the ball is clicked
            forceBarContainer.gameObject.SetActive(true);
        }
    }

    void OnMouseUp()
    {
        if (mouseDragging)
        {
            mouseDragging = false;

            // Calculate the force based on the distance between the mouse's initial position and the final position
            Vector3 mousePos = GetMousePos();
            Vector3 direction = mouseDragPos - mousePos;
            float force = Mathf.Clamp(direction.magnitude * forceMultiplier, 0, maxForce);
            force = Mathf.Lerp(0, force, 0.45f);

            // Make the ball non-kinematic so it gets affected by physics when it's shot
            rb.isKinematic = false;
            rb.AddForce(direction.normalized * force, ForceMode.Impulse);

            // Set ballShot to true so the player cannot spawn a new ball until the current one is shot
            ballShot = true;
            // Hide the force bar after the ball is shot
            forceBarContainer.gameObject.SetActive(false);
        }
    }

    public void GetForceBarUI(Image newForceBar, RectTransform newForceBarContainer)
    {
        // Get the force bar UI elements then hide them initially
        forceBar = newForceBar;
        forceBarContainer = newForceBarContainer;
        forceBarContainer.gameObject.SetActive(false);
    }

    private Vector3 GetMousePos()
    {
        // Get the mouse position in world space
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}

