using UnityEngine;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
