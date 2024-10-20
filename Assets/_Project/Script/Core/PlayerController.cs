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
        public float moveSpeed = 5f;         // Movement speed
        public float rotationSpeed = 10f;    // Rotation smoothing speed

        private Rigidbody rb;                // Reference to Rigidbody component
        private Vector2 movementInput;       // Store WASD/arrow key input

        private PlayerInputActions inputActions;

        void Awake()
        {
            // Initialize input actions and register movement callbacks
            inputActions = new PlayerInputActions();
            inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;

            rb = GetComponent<Rigidbody>();  // Get Rigidbody component
            rb.freezeRotation = true;        // Disable physics-based rotation
        }

        void OnEnable() => inputActions.Player.Enable();
        void OnDisable() => inputActions.Player.Disable();

        void FixedUpdate()
        {
            MoveAndRotatePlayer();
        }

        void MoveAndRotatePlayer()
        {
            // Create a 3D movement direction (X-Z plane)
            Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;

            if (moveDirection.magnitude >= 0.1f)  // If there is movement input
            {
                // Move the player using Rigidbody
                rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

                // Calculate the target angle based on movement direction
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

                // Smoothly rotate the character to face the target direction
                Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }
}