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

        [SerializeField] private float speed = 100f;

        void Start()
        {
            SpawnAsteroid();
        }

        public void SpawnAsteroid(AsteroidSize size=AsteroidSize.Small)
        {
            asteroidValue = size switch
            {
                AsteroidSize.Large => 20,
                AsteroidSize.Medium => 50,
                AsteroidSize.Small => 100,
                _    => asteroidValue
            };

            float tempScale = 1.0f;

            if (size == AsteroidSize.Small)
            {
                transform.localScale *= .33f;
                tempScale = 1.66f; 
            }
            if (size == AsteroidSize.Medium)
            {
                transform.localScale *= .66f;
                tempScale = 1.33f; 
            }
            // maybe scale as well?
            List<int> dirs = RandomizeDirections();
            rb.AddForce(directions[dirs[0]] * speed * tempScale);
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
            GameManager.Instance.UpdateScore(asteroidValue);
        }
    }
    public enum AsteroidSize
    {
        Large, Small, Medium
    }

}
