using UnityEngine;
using System;

namespace CurlyHooper
{
    [CreateAssetMenu(fileName = "NewScoreData", menuName = "Curly Hooper/Scriptable Objects/Score Data")]
    public class ScoreData : ScriptableObject
    {
        [SerializeField] private int _pointsPerBasket = 2;

        [NonSerialized] private int _currentPoints;
        public int CurrentPoints => _currentPoints;

        public event Action OnScoreChanged;

        public void AddScore()
        {
            _currentPoints += _pointsPerBasket;
            OnScoreChanged?.Invoke();
        }

        public void ResetScore()
        {
            _currentPoints = 0;
            OnScoreChanged?.Invoke();
        }

        public void OnAfterDeserialize()
        {
            _currentPoints = 0;
        }
    }
}
