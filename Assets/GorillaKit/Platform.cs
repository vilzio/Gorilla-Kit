using UnityEngine;

namespace GorillaKit
{
    public class Platform : MonoBehaviour
    {
        public PlatformPointSo[] points;
        
        private Vector3 destination;
        private int currentPoint;
        private Rigidbody rb;
        private Vector3 lastPosition;
        private float speed;
        private float moveSpeed;

        private void Start()
        {
            if (!gameObject.TryGetComponent(out rb))
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }
            
            rb.isKinematic = true;
            
            if (points is { Length: > 0 })
            {
                destination = points[currentPoint].point.position;
                speed = points[currentPoint].speed;
            }
            else
            {
                Debug.LogError($"No points defined for {this.name}");
            }
        }

        private void FixedUpdate()
        {
            MoveToPoint(destination);
        }

        private void MoveToPoint(Vector3 point)
        {
            lastPosition = rb.position;
            
            rb.MovePosition(Vector3.MoveTowards(rb.position, point, speed * Time.fixedDeltaTime));
            if (Vector3.Distance(rb.position, point) < 0.1f)
            {
                currentPoint = (currentPoint + 1) % points.Length;
                destination = points[currentPoint].point.position;
                speed = points[currentPoint].speed;
            }

            moveSpeed = Vector3.Distance(rb.position, lastPosition) / Time.fixedDeltaTime;
        }
        
        public void MoveObject(Collider other)
        {
            var objRb = other.attachedRigidbody;

            if (objRb)
                objRb.velocity += Vector3.one * moveSpeed;
        }
    }
}
