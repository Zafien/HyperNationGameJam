using System.Collections.Generic;
using Cinemachine;

using UnityEngine;

using Sirenix.OdinInspector;
using UnityEngine.Rendering;
using System;
using System.Collections;
using Cysharp.Threading.Tasks;


namespace NF.Main.Gameplay
{

    public class PlayerController : MonoExt
    {
        [TabGroup("BodyPartsStats")][SerializeField] public float moveSpeed = 5f;          // Player movement speed
        [TabGroup("BodyPartsStats")][SerializeField] public float rotationSpeed = 10f;     // Rotation speed for smooth turning

        public PlayerInputActions playerInputActions; 
        private Vector2 moveInput;                   
        public Rigidbody rb;                         
        public Transform cameraTransform;        

        private void Awake()
        {
/*            rb = GetComponent<Rigidbody>();        */     // Get the Rigidbody component
/*            rb.freezeRotation = true;       */            // Prevent physics-based rotation

            cameraTransform = Camera.main.transform;    // Get the main camera's transform

            // Initialize and enable the input actions
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            playerInputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        }


        private void OnEnable() => playerInputActions.Enable();
        private void OnDisable() => playerInputActions.Disable();

        private void FixedUpdate()
        {
            MovePlayer();    // Handle movement
            RotatePlayer();  // Handle rotation
        }

        private void MovePlayer()
        {
            // Get camera's forward and right directions (ignoring vertical movement)
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;

            // Normalize directions for consistent speed
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculate the movement direction based on input and camera orientation
            Vector3 moveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

            // Apply movement to the Rigidbody (locking Y-axis movement)
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, 0f, moveDirection.z * moveSpeed);
        }

        private void RotatePlayer()
        {
            if (moveInput != Vector2.zero)  // Rotate only if there is input
            {
                // Calculate the adjusted direction based on camera orientation
                Vector3 adjustedDirection = cameraTransform.forward * moveInput.y + cameraTransform.right * moveInput.x;
                adjustedDirection.y = 0;  // Ignore vertical rotation

                // Create a target rotation towards the movement direction
                Quaternion targetRotation = Quaternion.LookRotation(adjustedDirection);

                // Smoothly rotate the player towards the target rotation
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }
}