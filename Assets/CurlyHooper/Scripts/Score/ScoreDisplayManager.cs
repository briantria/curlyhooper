using UnityEngine;
using TMPro;

namespace CurlyHooper
{
    public class ScoreDisplayManager : MonoBehaviour
    {
        [SerializeField] private ScoreData _scoreData;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void OnEnable()
        {
            _scoreData.OnScoreChanged += UpdateDisplay;
            UpdateDisplay();
        }

        private void OnDisable()
        {
            _scoreData.OnScoreChanged -= UpdateDisplay;
        }

        private void UpdateDisplay()
        {
            _scoreText.text = $"SCORE: {_scoreData.CurrentPoints:D5}";
        }
    }
}
