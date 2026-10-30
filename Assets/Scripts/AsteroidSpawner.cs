using UnityEngine;
using System.Collections.Generic;
using System.Collections; 

namespace Asteroids
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private List<Vector3> spawnPoints;
        [SerializeField] private int spawnCount = 1;

        [SerializeField] private GameObject asteroidPrefab;

        private void Start()
        {
            GenerateAsteroids(spawnCount);
        }
        public void GenerateAsteroids(int amount)
        {
            // for now, just one.
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Vector3 randomPoint = spawnPoints[randomIndex];

            Instantiate(asteroidPrefab, randomPoint, transform.rotation);
        }
    }
}
