using UnityEngine;
using System;

namespace CurlyHooper
{
    public class RingTrigger : MonoBehaviour
    {
        [SerializeField] private bool _isTop;

        public static event Action<bool> OnRingTriggered;

        private void OnTriggerEnter(Collider other)
        {
            OnRingTriggered?.Invoke(_isTop);
        }
    }
}
