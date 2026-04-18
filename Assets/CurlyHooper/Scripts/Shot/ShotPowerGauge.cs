using UnityEngine;
using UnityEngine.UI;

namespace CurlyHooper
{
    public class ShotPowerGauge : MonoBehaviour
    {
        [SerializeField] private RectTransform _gaugeRect;
        [SerializeField] private Image _gaugeImage;
        [SerializeField] private ShotPowerData _shotPowerData;

        [Header("Visual Settings")]
        [SerializeField] private float _minSize = 20f;
        [SerializeField] private float _maxSize = 150f;
        [SerializeField] private Color _minColor = Color.yellow;
        [SerializeField] private Color _maxColor = Color.red;

        private void OnEnable()
        {
            _shotPowerData.OnPowerChanged += HandleShotPowerChange;
        }

        private void OnDisable()
        {
            _shotPowerData.OnPowerChanged -= HandleShotPowerChange;
        }

        private void Awake()
        {
            ResetGauge();
        }

        public void HandleShotPowerChange()
        {
            float progress = _shotPowerData.CurrentPower / _shotPowerData.MaxPower;
            float currentSize = Mathf.Lerp(_minSize, _maxSize, progress);
            _gaugeRect.sizeDelta = new Vector2(currentSize, currentSize);
            _gaugeImage.color = Color.Lerp(_minColor, _maxColor, progress);
        }

        public void ResetGauge()
        {
            _gaugeRect.sizeDelta = new Vector2(_minSize, _minSize);
        }
    }
}
