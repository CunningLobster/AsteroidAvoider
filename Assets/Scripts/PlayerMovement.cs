using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float screenSpaceBorderOffset = .1f;
    [SerializeField] private float rotationSpeed = 10f;

    private Rigidbody rb;
    private Camera mainCamera;

    private Vector3 movingDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void FixedUpdate()
    {
        if (movingDirection == Vector3.zero)
        {
            return;
        }
        
        rb.AddForce(movingDirection * forceMagnitude * Time.fixedDeltaTime, ForceMode.Force);
        rb.velocity = Vector3.ClampMagnitude(movingDirection, maxVelocity);
    }
    
   private void ProcessInput()
   {
       if (Touchscreen.current.primaryTouch.press.isPressed)
       {
           Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
           Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
           movingDirection = transform.position - worldPosition;
           movingDirection.z = 0;
           movingDirection.Normalize();
       }
       else
       {
           movingDirection = Vector3.zero;
       }
   }

   void KeepPlayerOnScreen()
   {
       Vector3 newPosition = transform.position;
       Vector3 viewPortPosition = mainCamera.WorldToViewportPoint(transform.position);

       if (viewPortPosition.x > 1)
       {
           newPosition.x = -newPosition.x + screenSpaceBorderOffset;
       }
       else if (viewPortPosition.x < 0)
       {
           newPosition.x = -newPosition.x - screenSpaceBorderOffset;
       }
       else if (viewPortPosition.y > 1)
       {
           newPosition.y = -newPosition.y + screenSpaceBorderOffset;
       }
       else if (viewPortPosition.y < 0)
       {
           newPosition.y = -newPosition.y - screenSpaceBorderOffset;
       }

       transform.position = newPosition;
   }

   void RotateToFaceVelocity()
   {
       if (rb.velocity == Vector3.zero)
       {
           return;
       }

       Quaternion targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.back);
       transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
   }

}
