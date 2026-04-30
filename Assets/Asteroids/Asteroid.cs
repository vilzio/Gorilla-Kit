using UnityEngine;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        public GameObject asteroidPrefab;
        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0));
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                Destroy(other.gameObject);
                Split();
                //Destroy(gameObject);
            }
        }

        void Split()
        {
            if (asteroidPrefab != null)
            {
                Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
                Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
            }
            else
                GameManager.Instance.asteroidsDestroyed++;
            Destroy(gameObject);
        }
    }
}
