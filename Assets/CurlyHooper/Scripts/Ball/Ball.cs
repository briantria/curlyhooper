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

        private void Update()
        {
            if (transform.position.y < -10f)
            {
                RigidBody.linearVelocity = Vector3.zero;
                RigidBody.angularVelocity = Vector3.zero;
                transform.position = new Vector3(0, 3, 0);
            }
        }
    }
}
