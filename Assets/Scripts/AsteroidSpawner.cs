using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Asteroids
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private List<Vector3> spawnPoints;
        [SerializeField] private int spawnCount = 1;

        [SerializeField] private List<GameObject> asteroidPrefabs;

        private void Start()
        {
            GenerateAsteroids(spawnCount);
        }
        public void GenerateAsteroids(int amount)
        {
            List<int> randomSpawnIndexes = RandomizeSpawnLocations();
            for (int i = 0; i < amount; i++)
            {
                Vector3 randomPoint = spawnPoints[randomSpawnIndexes[i]];
                int randomAsteroid = Random.Range(0, asteroidPrefabs.Count);
                Instantiate(asteroidPrefabs[randomAsteroid], randomPoint, transform.rotation);
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
