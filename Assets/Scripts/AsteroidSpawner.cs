using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Asteroids
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private List<Vector3> spawnPoints;

        [SerializeField] private List<GameObject> asteroidPrefabs;

        private void Start()
        {
            //GenerateAsteroids(spawnCount);
        }
        public void GenerateAsteroids(int amount, AsteroidSize size = AsteroidSize.Large )
        {
            List<int> randomSpawnIndexes = RandomizeSpawnLocations();
            for (int i = 0; i < amount; i++)
            {
                Vector3 randomPoint = spawnPoints[randomSpawnIndexes[i]];
                int randomAsteroid = Random.Range(0, asteroidPrefabs.Count);
                GameObject asteroidGO = Instantiate(asteroidPrefabs[randomAsteroid], randomPoint, transform.rotation);
                Asteroid a = asteroidGO.GetComponent<Asteroid>();

                //Asteroid a = Instantiate<Asteroid>(asteroidPrefabs[randomAsteroid], randomPoint, transform.rotation);
                a.SpawnAsteroid(size);
            }
        }

        private List<int> RandomizeSpawnLocations()
        {
            List<int> temp = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            temp = temp.OrderBy(x => Random.value).ToList();
            return temp;
        }
    }
}
