using UnityEngine;
using Unity.Cinemachine;

namespace CurlyHooper
{
    public class CameraJuice : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CinemachineCamera _vCam;
        [SerializeField] private CinemachineImpulseSource _impulseSource;
        [SerializeField] private ShotPowerData _shotPowerData;
        [SerializeField] private ScoreData _scoreData;

        [Header("FOV Settings")]
        [SerializeField] private float _defaultFOV = 60f;
        [SerializeField] private float _maxChargeFOV = 50f;
        [SerializeField] private float _fovLerpSpeed = 5f;

        [Header("Impulse Settings")]
        [SerializeField] private float _shotReleaseForce = 0.5f;
        [SerializeField] private float _scoreForce = 1.2f;

        private void OnEnable()
        {
            if (_scoreData != null) _scoreData.OnScoreChanged += HandleScoreJuice;
            if (_shotPowerData != null) _shotPowerData.OnPowerChanged += HandlePowerJuice;
        }

        private void OnDisable()
        {
            if (_scoreData != null) _scoreData.OnScoreChanged -= HandleScoreJuice;
            if (_shotPowerData != null) _shotPowerData.OnPowerChanged -= HandlePowerJuice;
        }

        private void Update()
        {
            if (_shotPowerData == null || _vCam == null)
            {
                return;
            }

            float powerPercent = _shotPowerData.CurrentPower / _shotPowerData.MaxPower;
            float targetFOV = Mathf.Lerp(_defaultFOV, _maxChargeFOV, powerPercent);
            _vCam.Lens.FieldOfView = Mathf.Lerp(_vCam.Lens.FieldOfView, targetFOV, Time.deltaTime * _fovLerpSpeed);
        }

        private void HandlePowerJuice()
        {
            if (_shotPowerData.CurrentPower <= 0.01f && _vCam.Lens.FieldOfView < _defaultFOV - 1f)
            {
                TriggerImpulse(_shotReleaseForce);
            }
        }

        private void HandleScoreJuice()
        {
            TriggerImpulse(_scoreForce);
        }

        private void TriggerImpulse(float force)
        {
            if (_impulseSource != null)
            {
                _impulseSource.GenerateImpulse(Random.insideUnitCircle.normalized * force);
            }
        }
    }
}