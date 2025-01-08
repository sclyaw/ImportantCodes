using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 8f; 
    public float minZoom = 5f; 
    public float maxZoom = 80f; 

    public float edgeScrollSpeed = 800f; 
    public float edgeScrollSpeedTwo = 100f;
    public float edgeScrollThreshold = 70f; // kenar kaydýrma esigi

    private Camera cam;
    private float targetZoom; 
    private float zoomVelocity; 

    
    private Vector3 targetPosition; 
    private Vector3 positionVelocity;

    void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize; 
        targetPosition = transform.position; 
    }

    void Update()
    {
        HandleZoom();

        
        if (Input.GetMouseButton(2)) 
        {
            HandleMouseScrollViewTool();
        }
        else
        {
            HandleEdgeScrolling();
        }
    }

    private void HandleZoom()
    {
        
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        
        if (scrollInput != 0f)
        {
            targetZoom -= scrollInput * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }

        // SmoothDamp!!
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref zoomVelocity, 0.1f);
    }

    private void HandleMouseScrollViewTool()
    {
        
        float moveX = Input.GetAxis("Mouse X") * edgeScrollSpeedTwo * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * edgeScrollSpeedTwo * Time.deltaTime;

        
        targetPosition -= new Vector3(moveX/3, moveY/3, 0);

        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref positionVelocity, 0.1f);
    }

    private void HandleEdgeScrolling()
    {
        Vector3 movement = Vector3.zero;

        
        if (Input.mousePosition.x <= edgeScrollThreshold)
        {
            movement.x = -edgeScrollSpeed * Time.deltaTime; 
        }
        else if (Input.mousePosition.x >= Screen.width - edgeScrollThreshold)
        {
            movement.x = edgeScrollSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= 20)
        {
            movement.y = -edgeScrollSpeed * Time.deltaTime; 
        }
        else if (Input.mousePosition.y >= Screen.height - edgeScrollThreshold)
        {
            movement.y = edgeScrollSpeed * Time.deltaTime; 
        }

        
        targetPosition = transform.position + movement;

        // SmoothDamp!!
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref positionVelocity, 1f);
    }
}


