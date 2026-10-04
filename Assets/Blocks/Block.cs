using GorillaKit;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Blocks
{
    public class Block : MonoBehaviour
    {
        public float blockSize = 0.5f;
        public float smallSize = 0.25f;
        public LayerMask collisionMask;

        private Grab grab;
        private Rigidbody rb;
        private XRGrabInteractable grabInteractable;

        private bool grabbed;
        private BlockRange blockRange;
        private bool wasInRange = false;

        private void Start()
        {
            blockRange = GetComponentInChildren<BlockRange>();
            if (!gameObject.TryGetComponent(out grab))
            {
                grab = gameObject.AddComponent<Grab>();
                Debug.LogWarning("Grab component missing, added automatically.");
            }
            if (!gameObject.TryGetComponent(out rb))
                rb = gameObject.AddComponent<Rigidbody>();
            if (!gameObject.TryGetComponent(out grabInteractable))
                grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
            
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.collider.name);
            if (ValidateCollision(collision.collider) && wasInRange && !grabbed)
            {
                Place();
            }
            wasInRange = false;
        }

        private void OnGrab(SelectEnterEventArgs args)
        {
            transform.localScale = Vector3.one * smallSize;
            rb.isKinematic = false;
            grabbed = true;
        }

        private void OnRelease(SelectExitEventArgs args)
        {
            grabbed = false;
            wasInRange = blockRange.InRange();
        }
        
        private void Place()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            transform.position = SnapToGrid(transform.position);
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one * blockSize;
        }
        
        private Vector3 SnapToGrid(Vector3 position)
        {
            Vector3 newPosition;
            var gridScale = blockSize/2;
            
            newPosition = new Vector3(
                Mathf.Round(position.x / gridScale) * gridScale,
                (Mathf.Round((position.y) / gridScale) * gridScale) + (blockSize/4),
                Mathf.Round(position.z / gridScale) * gridScale
            );
            
            return newPosition;
        }
        
        private bool ValidateCollision(Collider other)
        {
            return (collisionMask & (1 << other.gameObject.layer)) != 0;
        }
    }
}
