using UnityEngine;
using UnityEngine.InputSystem;

namespace CurlyHooper
{
    public class BallHandler : MonoBehaviour
    {
        [SerializeField] private Transform _hand;
        [SerializeField] private PickupTrigger _pickupTrigger;
        [SerializeField] private ShotCalculator _shotCalculator;
        [SerializeField] private ShotPowerData _shotPowerData;

        [Space(8)]
        [SerializeField] private float _pickupCooldownTime = 0.5f;

        [Header("Dribble Settings")]
        [SerializeField] private float _bounceHeight = 0.4f;
        [SerializeField] private float _bounceSpeed = 12f;

        private Ball _currentBall;
        private bool _isHoldingBall;
        private bool _isCharging;
        private float _nextPickupTime;

        #region Initialization
        private void OnEnable()
        {
            _pickupTrigger.OnZoneEntered += HandlePickupTrigger;
        }

        private void OnDisable()
        {
            _pickupTrigger.OnZoneEntered -= HandlePickupTrigger;
        }
        #endregion

        #region Pickup & Dribble Logic
        private void HandlePickupTrigger(Collider collider)
        {
            if (_isHoldingBall || Time.time < _nextPickupTime)
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

        private void HandleFakeDribble()
        {
            float bounce = Mathf.Abs(Mathf.Sin(Time.time * _bounceSpeed)) * _bounceHeight;
            _currentBall.transform.position = _hand.position + new Vector3(0, -bounce, 0);
            _currentBall.transform.Rotate(Vector3.right, 180f * Time.deltaTime);
        }
        #endregion

        #region Shooting Logic
        public void OnShoot(InputValue value)
        {
            if (!_isHoldingBall)
            {
                return;
            }

            if (value.isPressed)
            {
                StartCharging();
            }
            else
            {
                ReleaseBall();
            }
        }

        private void StartCharging()
        {
            _isCharging = true;
            _shotPowerData.SetCurrentPower(0);
            _currentBall.transform.position = _hand.position;
        }

        public void ReleaseBall()
        {
            if (!_isCharging)
            {
                return;
            }

            _isCharging = false;
            _isHoldingBall = false;

            Vector3 shootDir = _shotCalculator.GetCalculatedShootDirection(transform);

            _currentBall.IsHeld = false;
            _currentBall.SetPhysics(true);
            _currentBall.RigidBody.AddForce(shootDir * _shotPowerData.CurrentPower, ForceMode.Impulse);

            _nextPickupTime = Time.time + _pickupCooldownTime;
            _currentBall = null;
        }
        #endregion

        private void Update()
        {
            if (!_isHoldingBall || _currentBall == null)
            {
                return;
            }

            if (_isCharging)
            {
                _shotPowerData.ChargePower();
            }
            else
            {
                HandleFakeDribble();
            }
        }
    }
}
