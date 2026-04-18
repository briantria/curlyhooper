using UnityEngine;
using UnityEngine.InputSystem;

namespace CurlyHooper
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _moveSpeed = 7f;
        [SerializeField] private float _jumpHeight = 2.0f;
        [SerializeField] private float _airDrag = 0.5f;

        private CharacterController _controller;
        private Vector2 _moveInput;
        private Vector3 _verticalVelocity;
        private Vector3 _horizontalVelocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            ApplyMovement();
            ApplyGravity();
        }

        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void OnJump(InputValue value)
        {
            if (_controller.isGrounded)
            {
                _verticalVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }
        }

        private void ApplyMovement()
        {
            if (_controller.isGrounded)
            {
                // When on the ground, WASD dictates the velocity
                _horizontalVelocity = (transform.right * _moveInput.x + transform.forward * _moveInput.y) * _moveSpeed;
            }
            else
            {
                _horizontalVelocity = Vector3.Lerp(_horizontalVelocity, Vector3.zero, _airDrag * Time.deltaTime);
            }

            _controller.Move(_horizontalVelocity * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            if (_controller.isGrounded && _verticalVelocity.y < 0)
            {
                _verticalVelocity.y = -2f;
            }

            _verticalVelocity.y += _gravity * Time.deltaTime;
            _controller.Move(_verticalVelocity * Time.deltaTime);
        }
    }
}
