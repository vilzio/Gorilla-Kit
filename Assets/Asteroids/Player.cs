using UnityEngine;

namespace Asteroids
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float rotationSpeed;
        private bool rotating;
        public Transform firePoint;
        public GameObject bulletPrefab;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            if (rotating)
            {
                rb.MoveRotation(rb.rotation + (rotationSpeed * 100) * Time.deltaTime);
            }
        }
        
        public void Rotate(float speed)
        {
            rotating = true;
            rotationSpeed = speed;
        }
        
        public void StopRotate()
        {
            rotating = false;
        }
        
        public void Fire()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = firePoint.up * 20;
        }
    }
}
