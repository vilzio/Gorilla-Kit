using UnityEngine;

namespace Asteroids
{
    public class Spawner : MonoBehaviour
    {
        public GameObject asteroidPrefab;
        
        public void Spawn()
        {
            GameObject asteroid = Instantiate(asteroidPrefab, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), Quaternion.identity);
            Rigidbody2D asteroidRb = asteroid.GetComponent<Rigidbody2D>();
            asteroidRb.AddForce(new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0));
        }
    }
}
