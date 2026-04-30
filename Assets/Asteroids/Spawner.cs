using UnityEngine;

namespace Asteroids
{
    public class Spawner : MonoBehaviour
    {
        public GameObject asteroidPrefab;
        private float time;
        public float spawnTime;
        public float spawnTimeDecrmt;
        private int asteroids;

        private void Update()
        {
            if (GameManager.Instance.gameReset)
            {
                asteroids = 0;
                spawnTime -= spawnTimeDecrmt;
                GameManager.Instance.gameReset = false;
            }

            time += Time.deltaTime;
            if (time > spawnTime && asteroids < GameManager.Instance.maxAsteroids)
            {
                time = 0;
                Spawn();
            }
        }
        
        public void Spawn()
        {
            asteroids++;
            Instantiate(asteroidPrefab, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), Quaternion.identity);
        }
    }
}
