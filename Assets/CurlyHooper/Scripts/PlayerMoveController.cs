using UnityEngine;
using UnityEngine.InputSystem;

namespace CurlyHooper
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _gravity = -9.81f;

        private CharacterController _controller;
        private Vector2 _moveInput;
        private Vector3 _velocity;

        void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            ApplyMovement();
            ApplyGravity();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        private void ApplyMovement()
        {
            Vector3 move = transform.right * _moveInput.x + transform.forward * _moveInput.y;
            _controller.Move(move * _moveSpeed * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            if (_controller.isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _velocity.y += _gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}
