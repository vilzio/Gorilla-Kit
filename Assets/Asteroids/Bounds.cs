using UnityEngine;

namespace Asteroids
{
    public class Bounds : MonoBehaviour
    {
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Asteroid"))
            {
                Vector3 position = other.transform.position;
                position.x = -position.x;
                position.y = -position.y;
                other.transform.position = position;
            }
            
            if (other.CompareTag("Bullet"))
                Destroy(other.gameObject);
        }
    }
}
