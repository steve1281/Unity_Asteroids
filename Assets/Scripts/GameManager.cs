using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {
 
        private AsteroidSpawner spawner;
        private UIManager ui;
        private int score = 0;
        private int ships = 4;
        private int activePlayerBullets = 0;
        private int activeLargeAsteroids = 0;
        private int activeMediumAsteroids = 0;
        private int activeSmallAsteroids = 0;
        private int currentWave = 1;

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
            ui.UpdateShips(ships);
            ui.UpdateScore(score);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            spawner = FindAnyObjectByType<AsteroidSpawner>();
            ui = FindAnyObjectByType<UIManager>();
            ResetGame();
            SpawnWave();
        }
        private void ResetGame()
        {
            score = 0;
            currentWave = 1;
            ships = 4;
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



        public void SpawnWave()
        {
            Debug.Log($"GameManager::SpawnWave current wave {currentWave}");
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
            ships--;
            if (ships < 0)
            {
                Debug.Log("--- Game Over ----");
                Destroy(p);
                StartCoroutine(RestartGame());
            }
            else
            {
                Debug.Log("--- Player scheduled for reincarnation ----");
                Player player = p.GetComponent<Player>();
                if (player == null) return;
                p.SetActive(false); // make player go away
                StartCoroutine(ReincarnatePlayer(p));
            }
        }

        private IEnumerator ReincarnatePlayer(GameObject p)
        {
            // we should check if it is safe
            yield return new WaitForSecondsRealtime(2.5f);
            p.SetActive(true);
        }
        private IEnumerator RestartGame()
        {
            yield return new WaitForSecondsRealtime(10f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
