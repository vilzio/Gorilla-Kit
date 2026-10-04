using UnityEngine;

namespace GorillaKit
{
    public class PlatformPointSo : MonoBehaviour
    {
        public Transform point;
        public float speed = 1f;

        private void Awake()
        {
            if (point == null)
            {
                point = transform;
            }
        }
    }
}
