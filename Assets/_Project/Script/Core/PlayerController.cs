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
        [TabGroup("Move Stats")][SerializeField] public float moveSpeed = 5f;         // Movement speed
        public float rotationSpeed = 10f;    // Rotation smoothing speed
        private Rigidbody rb;                
        private Vector2 movementInput;    
        private PlayerInputActions inputActions;

        public Transform ExchangeObject;

        void Awake()
        {
            // Initialize input actions and register movement callbacks
            inputActions = new PlayerInputActions();
            inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;
            rb = GetComponent<Rigidbody>();  
   
        }

        void OnEnable() => inputActions.Player.Enable();
        void OnDisable() => inputActions.Player.Disable();


        private void Update()
        {
            Exchange();
        }
        void FixedUpdate()
        {
            MoveAndRotatePlayer();
        }

        void MoveAndRotatePlayer()
        {
           
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


        public void Exchange()
        {
            //Testing purposes Only
            if (Input.GetKeyDown(KeyCode.V))
            {
                Vector3 tempPosition = this.transform.position;

                // Swap positions
                this.transform.position = ExchangeObject.position;
                ExchangeObject.position = tempPosition;
            }
           
        }
    }
}