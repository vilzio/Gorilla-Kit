using System;
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
            if (Power.Instance.isOn)
            {
                if (rotating)
                {
                    rb.MoveRotation(rb.rotation + (rotationSpeed * 100) * Time.deltaTime);
                }
            }
            else
            {
                rb.MoveRotation(rb.rotation = 0);
                gameObject.SetActive(false);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            GameObject[] asteroid;
            
            if (other.CompareTag("Asteroid"))
            {
                asteroid = GameObject.FindGameObjectsWithTag("Asteroid");
                foreach (GameObject aster in asteroid)
                    Destroy(aster);
                GameManager.Instance.gameReset = true;
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
