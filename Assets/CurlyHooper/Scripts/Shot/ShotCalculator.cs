using UnityEngine;

namespace CurlyHooper
{
    public class ShotCalculator : MonoBehaviour
    {
        [SerializeField] private Transform _hoopTarget;
        [SerializeField] private float _baseArcBias = 0.5f;
        [SerializeField] private float _distanceArcMultiplier = 0.1f;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public Vector3 GetCalculatedShootDirection(Transform playerTransform)
        {
            if (_hoopTarget == null)
            {
                return Camera.main.transform.forward;
            }

            Vector3 flatPlayerPos = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);
            Vector3 flatHoopPos = new Vector3(_hoopTarget.position.x, 0, _hoopTarget.position.z);
            Vector3 directionToHoop = (flatHoopPos - flatPlayerPos).normalized;
            float distance = Vector3.Distance(flatPlayerPos, flatHoopPos);
            float dynamicArc = _baseArcBias + (distance * _distanceArcMultiplier);
            Vector3 finalDir = Vector3.Lerp(_camera.transform.forward, directionToHoop, 0.3f);
            finalDir.y += dynamicArc;

            return finalDir.normalized;
        }
    }
}
