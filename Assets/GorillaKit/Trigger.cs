using UnityEngine;
using UnityEngine.Events;

namespace GorillaKit
{
    public class Trigger : MonoBehaviour
    {
        [Header("Settings")]
        public UnityEvent<Collider> onEnter;
        public UnityEvent<Collider> onExit;
        public LayerMask collisionMask;
        public bool debug = true;
        public bool toggle;

        private bool isActive;

        private void OnTriggerEnter(Collider other)
        {
            if (ValidateCollision(other))
            {
                Enter(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (ValidateCollision(other))
            {
                Exit(other);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void Enter(Collider other)
        {
            if (toggle)
            {
                isActive = !isActive;
                if (isActive)
                {
                    onEnter.Invoke(other);
                    DebugLog($"{gameObject.name} toggled by {other.gameObject.name}");
                }
                else
                {
                    onExit.Invoke(other);
                    DebugLog($"{gameObject.name} untoggled by {other.gameObject.name}");
                }
            }
            else if (!isActive)
            {
                onEnter.Invoke(other);
                isActive = true;
                DebugLog($"{gameObject.name} entered by {other.gameObject.name}");
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void Exit(Collider other)
        {
            if (!toggle && isActive)
            {
                onExit.Invoke(other);
                isActive = false;
                DebugLog($"{gameObject.name} exited by {other.gameObject.name}");
            }
        }

        private bool ValidateCollision(Collider other)
        {
            return (collisionMask & (1 << other.gameObject.layer)) != 0;
        }
        
        private void DebugLog(string msg)
        {
            if (debug)
            {
                Debug.Log(msg);
            }
        }
    }
}
