using UnityEngine;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public int asteroidsDestroyed;
        public int maxAsteroids;
        private int defaultMaxAsteroids;
        public int maxAsteroidsIncrmt;
        public bool gameReset;
        public int rounds;

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
            
            defaultMaxAsteroids = maxAsteroids;
        }

        private void Update()
        {
            if (Power.Instance.isOn)
            {
                if (asteroidsDestroyed == maxAsteroids * 4)
                {
                    Debug.Log("Game Over");
                    rounds++;
                    gameReset = true;
                }

                if (gameReset)
                {
                    asteroidsDestroyed = 0;
                    maxAsteroids = defaultMaxAsteroids + maxAsteroidsIncrmt * rounds;
                    gameReset = false;
                }
            }
            else
            {
                asteroidsDestroyed = 0;
                maxAsteroids = defaultMaxAsteroids;
            }
        }
    }
}
