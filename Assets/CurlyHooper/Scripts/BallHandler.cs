using UnityEngine;

namespace CurlyHooper
{
    public class BallHandler : MonoBehaviour
    {
        [SerializeField] private Transform _hand;
        [SerializeField] private float _bounceHeight = 0.4f;
        [SerializeField] private float _bounceSpeed = 12f;

        [Space(8)]
        [SerializeField] private PickupTrigger _pickupTrigger;

        private Ball _currentBall;
        private bool _isHoldingBall;

        private void OnEnable()
        {
            _pickupTrigger.OnZoneEntered += HandlePickupTrigger;
        }

        private void OnDisable()
        {
            _pickupTrigger.OnZoneEntered -= HandlePickupTrigger;
        }

        private void HandlePickupTrigger(Collider collider)
        {
            Debug.Log($"on trigger enter: {collider.name}");
            if (_isHoldingBall)
            {
                return;
            }

            Ball ball = collider.GetComponent<Ball>();
            if (ball != null && !ball.IsHeld)
            {
                PickupBall(ball);
            }
        }

        private void PickupBall(Ball ball)
        {
            _currentBall = ball;
            _isHoldingBall = true;
            _currentBall.IsHeld = true;
            _currentBall.SetPhysics(false);
        }

        private void Update()
        {
            if (_isHoldingBall && _currentBall != null)
            {
                HandleFakeDribble();
            }
        }

        private void HandleFakeDribble()
        {
            float bounce = Mathf.Abs(Mathf.Sin(Time.time * _bounceSpeed)) * _bounceHeight;
            _currentBall.transform.position = _hand.position + new Vector3(0, -bounce, 0);
            _currentBall.transform.Rotate(Vector3.right, 180f * Time.deltaTime);
        }

        public void ReleaseBall(Vector3 force)
        {
            if (!_isHoldingBall)
            {
                return;
            }

            _currentBall.IsHeld = false;
            _currentBall.SetPhysics(true);
            _currentBall.RigidBody.AddForce(force, ForceMode.Impulse);

            _currentBall = null;
            _isHoldingBall = false;
        }
    }
}
