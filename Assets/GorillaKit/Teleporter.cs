using UnityEngine;

namespace GorillaKit
{
    public class Teleporter : MonoBehaviour
    {
        public Transform target;
        
        public void Teleport(Collider other)
        {
            var objRb = other.attachedRigidbody;
            if (objRb)
            {
                objRb.velocity += Vector3.zero;
                objRb.angularVelocity = Vector3.zero;
                objRb.MovePosition(target.position);
                objRb.MoveRotation(target.rotation);
            }
        }
    }
}
