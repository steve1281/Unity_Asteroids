using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {
 
        private AsteroidSpawner spawner;
        private UIManager ui;
        private Saucer saucer = null;

        public AudioManager audioManager;

        private int score = 0;
        private int highScore = 0;
        private int ships = 4;
        private int activePlayerBullets = 0;
        private int activeLargeAsteroids = 0;
        private int activeMediumAsteroids = 0;
        private int activeSmallAsteroids = 0;
        private int currentWave = 1;
        private bool activeSaucer = false;
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
            if (score > highScore)
            {
                highScore = score;
            }
            ui.UpdateHighScore(highScore);
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
            saucer = FindAnyObjectByType<Saucer>();
            audioManager = FindAnyObjectByType<AudioManager>();
            ResetGame();
            SpawnWave();
            StartCoroutine(SpawnSaucers()); // needs to be a coroutine I think
        }
        private void ResetGame()
        {

            score = 0;
            currentWave = 1;
            ships = 4;
            activePlayerBullets = 0;
            activeLargeAsteroids = 0;
            activeMediumAsteroids = 0;
            activeSmallAsteroids = 0;
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


        public void SaucerEscaped(GameObject saucer)
        { 
            activeSaucer = false;
            Destroy(saucer);
            currentSaucer = null;   // why doesn't the Destroy do this? copy I guess
        }

        public void SaucerDestroyed(GameObject saucer)
        {
            Saucer s = saucer.GetComponent<Saucer>();
            if (s == null) return;
            SaucerSize size = s.GetSize();
            int value = 0;
            if (size == SaucerSize.Large)
            {
                value = 200;
            } else if (size == SaucerSize.Small) 
            {
                value = 1000;
            }

            UpdateScore(value);
            activeSaucer = false;
            Destroy(saucer);
            currentSaucer = null;   // why doesn't the Destroy do this? copy I guess
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
                //Debug.Log("--- Game Over ----");
                Destroy(p);
                ui.ToggleGameOver();
                StartCoroutine(RestartGame());
            }
            else
            {
                //Debug.Log("--- Player scheduled for reincarnation ----");
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

        private Saucer currentSaucer =null;
        private IEnumerator SpawnSaucers()
        {
            while (true)
            {
                yield return (new WaitForSecondsRealtime(15));
                if (!activeSaucer)
                {
                    if (activeLargeAsteroids +  activeSmallAsteroids + activeMediumAsteroids < 2)
                    {
                        currentSaucer = saucer.SpawnSaucer(SaucerSize.Small);
                    } else
                    {
                        currentSaucer = saucer.SpawnSaucer(SaucerSize.Large);
                    }
                    activeSaucer = true;
                } 
            }
        }
        public void FireSaucerBullet()
        {
            //Debug.Log("GameManager::FireSaucerBullet called.");
            if (currentSaucer != null) //activeSaucer)
            {
                Debug.Log($"GameManager::FireSaucerBullet currentSaucer is active at {currentSaucer.transform.position}.");
                Player p = FindAnyObjectByType<Player>();
                if (p != null) 
                {
                    //Debug.Log("GameManager::FireSaucerBullet player located/targeted.");
                    currentSaucer.FireBullet(p.transform);
                }
            }
        }
    }
}
