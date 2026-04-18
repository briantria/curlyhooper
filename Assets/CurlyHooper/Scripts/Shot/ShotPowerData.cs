using UnityEngine;
using System;

namespace CurlyHooper
{
    [CreateAssetMenu(fileName = "NewShotPowerData", menuName = "Curly Hooper/Scriptable Objects/Shot Power Data")]
    public class ShotPowerData : ScriptableObject
    {
        [SerializeField] private int _maxPower = 10;
        [SerializeField] private float _powerChargeSpeed = 8f;
        [SerializeField] private float _upwardBias = 0.4f;

        [NonSerialized] private float _currentPower;

        public int MaxPower => _maxPower;
        public float PowerChargeSpeed => _powerChargeSpeed;
        public float UpwardBias => _upwardBias;
        public float CurrentPower => _currentPower;

        public void SetCurrentPower(float power)
        {
            _currentPower = Mathf.Clamp(power, 0, _maxPower);
        }

        public void ChargePower()
        {
            _currentPower = Mathf.MoveTowards(_currentPower, _maxPower, _powerChargeSpeed * Time.deltaTime);
        }

        public void OnAfterDeserialize()
        {
            _currentPower = 0;
        }
    }
}
