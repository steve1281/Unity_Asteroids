using UnityEngine;

namespace Asteroids
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private float lifetime = 1.0f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float bulletSpeed = 500f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void Start()
        {
            Destroy(gameObject, lifetime);
        }
        public void Fire(Vector3 direction)
        { 
            rb.AddForce(direction * bulletSpeed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the hit object belongs to the "Player" layer
            if (other.gameObject.layer == Layers.Player)
            {
                GameManager.Instance.PlayerDestroyed(other.gameObject);
                Destroy(gameObject);
            }
            if (other.gameObject.layer == Layers.Teleporter)
            {
                Destroy(gameObject);
            }
        }

    }
}
