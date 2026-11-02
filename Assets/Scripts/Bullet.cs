using UnityEngine;

namespace Asteroids
{
    public class Bullet : MonoBehaviour
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
            rb.AddForce(gameObject.transform.right * bulletSpeed);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == Layers.Teleporter) return; // ignore.

            // Check if the hit object belongs to the "Enemy" layer
            if (other.gameObject.layer == Layers.Enemy)
            {
                Debug.Log("Bullet::OnTriggerEnter2D Hit enemy; destroy it for now.");
                GameManager.Instance.AsteroidDestroyed(other.gameObject);
                //Destroy(other.gameObject);
            }
            // if bullet collides with anything; remove it
            Debug.Log("Bullet::OnTriggerEnter2D collided and removed bullet.");
            Destroy(gameObject);
        }
        private void OnDestroy()
        {
            GameManager.Instance.SubPlayerBullet();
        }

    }
}
