using UnityEngine;
using UnityEngine.InputSystem;

namespace CurlyHooper
{
    public class PlayerLookAtController : MonoBehaviour
    {
        [SerializeField] private Transform _head;
        [SerializeField] private float _lookSensitivity = 0.1f;
        [SerializeField] private float _upperLookLimit = -80f;
        [SerializeField] private float _lowerLookLimit = 80f;

        private Transform _body;
        private float _headRotation = 0f;

        private void Awake()
        {
            _body = GetComponent<Transform>();
        }

        void Start()
        {
            LockCursor();
        }
        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void OnLook(InputValue value)
        {
            Vector2 lookInput = value.Get<Vector2>();
            float mouseX = lookInput.x * _lookSensitivity;
            float mouseY = lookInput.y * _lookSensitivity;

            _headRotation -= mouseY;
            _headRotation = Mathf.Clamp(_headRotation, _upperLookLimit, _lowerLookLimit);

            _body.Rotate(Vector3.up * mouseX);
            _head.localRotation = Quaternion.Euler(_headRotation, 0f, 0f);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}