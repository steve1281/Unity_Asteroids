using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private List<Vector3> directions;

        private int asteroidValue =0;

        private AsteroidSize size = AsteroidSize.Large;

        void Start()
        {
            SpawnAsteroid();
        }

        public void SpawnAsteroid(AsteroidSize size=AsteroidSize.Large)
        {
            asteroidValue = size switch
            {
                AsteroidSize.Large => 20,
                AsteroidSize.Medium => 50,
                AsteroidSize.Small => 100,
                _    => asteroidValue
            };

            // maybe scale as well?
            List<int> dirs = RandomizeDirections();
            rb.AddForce(directions[dirs[0]] * 100f);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == Layers.Player)
            {
                Debug.Log("Asteroid::OnTriggerEnter2D Hit player; destroy it for now.");
                Destroy(other.gameObject);
            }
        }
        private List<int> RandomizeDirections()
        {
            List<int> temp = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            temp = temp.OrderBy(x => Random.value).ToList();
            return temp;
        }
        private void OnDestroy()
        {
            GameManager.Instance.UpdateScore(20);
        }
    }
    public enum AsteroidSize
    {
        Large, Small, Medium
    }

}
