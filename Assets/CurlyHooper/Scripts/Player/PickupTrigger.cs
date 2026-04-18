using UnityEngine;
using System;

namespace CurlyHooper
{
    public class PickupTrigger : MonoBehaviour
    {
        public event Action<Collider> OnZoneEntered;

        private void OnTriggerEnter(Collider other)
        {
            OnZoneEntered?.Invoke(other);
        }
    }
}
