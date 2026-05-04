using UnityEngine;

namespace Asteroids
{
    public class Power : MonoBehaviour
    {
        public static Power Instance;
        public bool isOn;
        public Spawner spawner;
        public Player player;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PowerOn()
        {
            isOn = true;
            spawner.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
        }
        
        public void PowerOff()
        {
            isOn = false;
        }
    }
}
