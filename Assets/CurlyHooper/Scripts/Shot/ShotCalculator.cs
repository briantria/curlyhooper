using UnityEngine;

namespace CurlyHooper
{
    public class ShotCalculator : MonoBehaviour
    {
        [SerializeField] private Transform _hoopTarget;
        [SerializeField] private float _baseArcBias = 0.5f;
        [SerializeField] private float _distanceArcMultiplier = 0.1f;
        [SerializeField, Range(0, 1)] private float _aimAssistStrength = 0.4f;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public Vector3 GetCalculatedShootDirection(Transform playerTransform, Vector3 ballWorldPos)
        {
            // 1. Find the point the crosshair is looking at (50 meters away)
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Vector3 focalPoint = ray.GetPoint(10f);

            // 2. Calculate direction from the BALL (on the right) to that focal point
            // This creates the "inward" angle so the ball moves toward the center
            Vector3 convergenceDir = (focalPoint - ballWorldPos).normalized;

            if (_hoopTarget == null)
            {
                return convergenceDir;
            }

            // 3. Get horizontal direction to hoop for magnetism
            Vector3 flatPlayerPos = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);
            Vector3 flatHoopPos = new Vector3(_hoopTarget.position.x, 0, _hoopTarget.position.z);
            Vector3 directionToHoop = (flatHoopPos - flatPlayerPos).normalized;

            // 4. Blend the converged direction with the hoop direction
            Vector3 finalDir = Vector3.Lerp(convergenceDir, directionToHoop, _aimAssistStrength);

            // 5. Add the vertical arc
            float distance = Vector3.Distance(flatPlayerPos, flatHoopPos);
            float dynamicArc = _baseArcBias + (distance * _distanceArcMultiplier);
            finalDir.y += dynamicArc;

            return finalDir.normalized;
        }
    }
}
