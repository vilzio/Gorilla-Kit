using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class Spawner : MonoBehaviour
    {
        public GameObject asteroidPrefab;
        private float time;
        public float spawnTime;
        private float defaultSpawnTime;
        public float spawnTimeDecrmt;
        private int asteroids;

        void Awake()
        {
            defaultSpawnTime = spawnTime;
        }
        
        private void Update()
        {
            TogglePower();
            
            if (GameManager.Instance.gameReset)
            {
                asteroids = 0;
                spawnTime = defaultSpawnTime - spawnTimeDecrmt * GameManager.Instance.rounds;
            }

            time += Time.deltaTime;
            if (time > spawnTime && asteroids < GameManager.Instance.maxAsteroids)
            {
                time = 0;
                Spawn();
            }
        }

        void TogglePower()
        {
            if (Power.Instance.isOn == false)
            {
                time = 0;
                spawnTime = defaultSpawnTime;
                asteroids = 0;
                gameObject.SetActive(false);
            }
        }
        
        public void Spawn()
        {
            asteroids++;
            Instantiate(asteroidPrefab, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), Quaternion.identity);
        }
    }
}
