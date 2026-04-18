using System.Collections;
using UnityEngine;

namespace CurlyHooper
{
    public class RingSensor : MonoBehaviour
    {
        [SerializeField] private ScoreData _scoreData;
        [SerializeField] private ParticleSystem _scoreParticles;

        private bool _didPassTopTrigger;
        private Coroutine _topTriggerRoutine;

        private void OnEnable()
        {
            RingTrigger.OnRingTriggered += HandleRingTriggered;
        }

        private void OnDisable()
        {
            RingTrigger.OnRingTriggered += HandleRingTriggered;
        }

        private void HandleRingTriggered(bool isTop)
        {
            if (isTop)
            {
                TopRingTriggered();
            }
            else
            {
                BottomRingTriggered();
            }
        }

        public void TopRingTriggered()
        {
            if (_topTriggerRoutine != null)
            {
                StopCoroutine(_topTriggerRoutine);
            }

            _topTriggerRoutine = StartCoroutine(TopTriggerRoutine());
        }

        public void BottomRingTriggered()
        {
            if (_didPassTopTrigger)
            {
                ScoreBasket();
            }
        }

        private IEnumerator TopTriggerRoutine()
        {
            _didPassTopTrigger = true;
            yield return new WaitForSeconds(0.6f);
            _didPassTopTrigger = false;
        }

        private void ScoreBasket()
        {
            _didPassTopTrigger = false;
            _scoreData.AddScore();
            _scoreParticles.Play();
        }

    }
}
