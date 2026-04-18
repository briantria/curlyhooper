using UnityEngine;

namespace CurlyHooper
{
    public class Ball : MonoBehaviour
    {
        public Rigidbody RigidBody { get; private set; }
        public bool IsHeld { get; set; }

        private void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
        }

        public void SetPhysics(bool enabled)
        {
            RigidBody.isKinematic = !enabled;
            if (!enabled)
            {
                RigidBody.linearVelocity = Vector3.zero;
            }
        }
    }
}
