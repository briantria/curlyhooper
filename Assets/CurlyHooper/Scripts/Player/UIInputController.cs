using UnityEngine;
using UnityEngine.InputSystem;

namespace CurlyHooper
{
    public class UIInputController : MonoBehaviour
    {
        public void OnCancel(InputValue value)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                ToggleCursor(true);
            }
            else
            {
                ToggleCursor(false);
            }
        }

        private void ToggleCursor(bool isUnlocked)
        {
            Cursor.lockState = isUnlocked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isUnlocked;
        }

        void Update()
        {
            if (Cursor.lockState != CursorLockMode.Locked &&
                Mouse.current.leftButton.wasPressedThisFrame)
            {
                ToggleCursor(false);
            }
        }
    }
}
