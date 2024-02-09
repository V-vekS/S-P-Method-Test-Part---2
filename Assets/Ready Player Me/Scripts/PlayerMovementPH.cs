using ReadyPlayerMe.Core; // Assuming ReadyPlayerMe.Core is needed for Avatar-related functionality
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovementScript
{
    public class PlayerMovementPH : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;

        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference runAction;

        [SerializeField] public Animator animator;
        [SerializeField] public Transform avatar;

        [SerializeField] Transform relativeCamera;

        [SerializeField] public float speed;

        private Vector2 inputMoveVector;

        [SerializeField] GameComponentManager gameComponentManager;


        private void Awake()
        {
            gameComponentManager.playerMovement = this;
        }

        private void OnEnable()
        {
            // Enable input actions when this component is enabled
            moveAction.action.Enable();
            runAction.action.Enable();
        }

        private void OnDisable()
        {
            // Disable input actions when this component is disabled
            moveAction.action.Disable();
            runAction.action.Disable();
        }

        // Method to set the avatar and animator externally (for example, by AvatarLoader)
        public void SetAvatarAndAnimator(Transform newAvatar, Animator newAnimator)
        {
            avatar = newAvatar;
            animator = newAnimator;
        }

        private void Update()
        {
            // Log to confirm the script is working
            Debug.Log("PlayerMovementScript Working");

            // Adjust speed based on whether the run action is pressed
            if (runAction.action.IsPressed())
            {
                speed = 6;
            }
            else
            {
                speed = 2;
            }

            // Get camera forward and right vectors, and normalize them
            Vector3 cameraForward = relativeCamera.forward;
            Vector3 cameraRight = relativeCamera.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward = cameraForward.normalized;
            cameraRight = cameraRight.normalized;

            // Read input values for movement
            inputMoveVector = moveAction.action.ReadValue<Vector2>();

            // Calculate the movement direction based on camera orientation
            Vector3 moveDirection = (cameraForward * inputMoveVector.y + cameraRight * inputMoveVector.x);
            moveDirection = moveDirection.normalized;

            // Move the character controller and set animator parameter
            controller.SimpleMove(moveDirection * speed);
            animator.SetFloat("Magnitude", (moveDirection * speed).magnitude);

            // Rotate the avatar to face the movement direction if not idle
            if (moveDirection != Vector3.zero)
            {
                avatar.forward = moveDirection;
                //Debug.Log("Avatar face forward");
            }
        }
    }
}
