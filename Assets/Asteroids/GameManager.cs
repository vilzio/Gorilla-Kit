using UnityEngine;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public int asteroidsDestroyed;
        public int maxAsteroids;
        public int maxAsteroidsIncrmt;
        public bool gameReset;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Update()
        {
            if (asteroidsDestroyed == maxAsteroids * 4)
            {
                Debug.Log("Game Over");
                asteroidsDestroyed = 0;
                maxAsteroids += maxAsteroidsIncrmt;
                gameReset = true;
            }
        }
    }
}
