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
    private TrailRenderer trailRenderer;
    private Image forceBar;
    private RectTransform forceBarContainer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        mainCamera = Camera.main;

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }

    public void GetForceBarUI(Image newForceBar, RectTransform newForceBarContainer)
    {
        forceBar = newForceBar;
        forceBarContainer = newForceBarContainer;
        forceBarContainer.gameObject.SetActive(false);
    }

    void Update()
    {
        if (mouseDragging)
        {
            Vector3 mousePos = GetMousePos();

            previousMousePos = Input.mousePosition;

            Vector3 direction = mouseDragPos - mousePos;
            float magnitude = Mathf.Min(direction.magnitude * forceMultiplier, maxForce);

            forceBar.fillAmount = magnitude / maxForce;
        }
    }

    void OnMouseDown()
    {
        if (!mouseDragging)
        {
            mouseDragging = true;
            rb.isKinematic = true;
            mouseDragPos = GetMousePos();
            forceBarContainer.gameObject.SetActive(true);
        }
    }

    void OnMouseUp()
    {
        if (mouseDragging)
        {
            mouseDragging = false;

            Vector3 mousePos = GetMousePos();
            Vector3 direction = mouseDragPos - mousePos;

            float force = Mathf.Clamp(direction.magnitude * forceMultiplier, 0, maxForce);

            rb.isKinematic = false;
            rb.AddForce(direction.normalized * force, ForceMode.Impulse);

            if (trailRenderer != null)
            {
                trailRenderer.enabled = true;
            }

            ballShot = true;

            Debug.Log("Ball Shot with force: " + force);

            forceBarContainer.gameObject.SetActive(false);
        }
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}

