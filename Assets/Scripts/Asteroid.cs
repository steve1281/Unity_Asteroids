using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private List<Vector3> directions;

        void Start()
        {
            SpawnAsteroid();
        }

        public void SpawnAsteroid()
        {
            List<int> dirs = RandomizeDirections();
            //rb.AddForce(gameObject.transform.right * 100f);
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
            Debug.Log("--- ==== Booom ==== ---");
        }
    }
}
