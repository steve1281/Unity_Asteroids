using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private List<Vector3> directions;
        [SerializeField] private AsteroidSpawner asteroidSpawner;

        private AsteroidSize size; // = AsteroidSize.Large;

        [SerializeField] private float speed = 100f;

        public void SpawnAsteroid(AsteroidSize size)
        {
            this.size = size;

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
            if (!gameObject.scene.isLoaded) return;

            if (GameManager.Instance != null) 
                GameManager.Instance.AsteroidDestroyed(size);
        }
    }
    public enum AsteroidSize
    {
        Large, Small, Medium
    }

}
