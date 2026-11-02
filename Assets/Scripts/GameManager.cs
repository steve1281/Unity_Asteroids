using TMPro;
using UnityEngine;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Score;
        public int score = 0;
        [SerializeField] private TextMeshProUGUI Ships;
        public int ships = 4;
        private string shipReference = "^^^^^^^^^^^^";

        private int activePlayerBullets = 0;

        private static GameManager instance;
        public static GameManager Instance { get => instance; private set => instance = value; }
        private void Awake()
        {
            if (Instance != null && Instance !=this) 
            { 
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            Score.text = score.ToString("000000000");
            Ships.text = shipReference[^ships..];
        }
        private void Start()
        {
            SpawnWave();
        }

        public void UpdateScore(int amount = 1)
        {
            score += amount;
        }
        public void UpdateShips(int amount = 1)
        {
            ships += amount;
        }
        public void AddPlayerBullet()
        {
            activePlayerBullets++;
        }
        public void SubPlayerBullet()
        {
            activePlayerBullets--;
        }
        public bool CanPlayerFire()
        {
            if (activePlayerBullets <4)
            {
                return true;
            }
            return false;
        }

        [SerializeField] private AsteroidSpawner spawner;
        private int activeLargeAsteroids = 0;
        private int activeMediumAsteroids = 0;
        private int activeSmallAsteroids = 0;
        private int currentWave = 1;

        public void SpawnWave()
        {
            int count = currentWave switch
            {
                1 => 1, // 4
                2 => 2, // 6,
                3 => 3, // 8,
                _ => 4  // 9
            };
            spawner.GenerateAsteroids(count, AsteroidSize.Large);
            activeLargeAsteroids = count;
        }

        public void AsteroidDestroyed(GameObject asteroid) //, AsteroidSize size)
        {
            Asteroid a = asteroid.GetComponent<Asteroid>();
            if (a == null) return;
            AsteroidSize size = a.GetSize();

            if (spawner == null) return;

            int asteroidValue = 0;

            if (size == AsteroidSize.Large)
            {
                Debug.Log("GameManager:: AsteroidDestroyed and it was Large");
                asteroidValue = 20;
                activeLargeAsteroids--;
                spawner.GenerateAsteroids(2, AsteroidSize.Medium);
                activeMediumAsteroids += 2;
            } else if (size == AsteroidSize.Medium)
            {
                Debug.Log("GameManager:: AsteroidDestroyed and it was Medium");
                asteroidValue = 50;
                activeMediumAsteroids--;
                spawner.GenerateAsteroids(2, AsteroidSize.Small);
                activeSmallAsteroids += 2;
            } else
            {
                Debug.Log("GameManager:: AsteroidDestroyed and it was Small");
                asteroidValue = 100;
                activeSmallAsteroids--;
            }

            if (activeLargeAsteroids+ activeMediumAsteroids+activeSmallAsteroids == 0)
            {
                currentWave++;
                SpawnWave();
            }
            UpdateScore(asteroidValue);
            Destroy(asteroid);


        }
        public void PlayerDestroyed(GameObject p)
        {
            Destroy(p);

        }
    }
}
