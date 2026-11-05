using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Saucer : MonoBehaviour
    {
        [SerializeField] private Saucer saucerPrefab;
        [SerializeField] private float speed = 2.5f;

        [SerializeField] private GameObject cannon;
        [SerializeField] private EnemyBullet bulletPrefab;

        private SaucerSize size;

        private List<Vector3> rightDirections = new List<Vector3>()
        {
            new Vector3(-6.5f, 4.5f, 0f),
            new Vector3(-6.5f, 3.5f, 0f),
            new Vector3(-6.5f, 2.5f, 0f),
            new Vector3(-6.5f, 1.5f, 0f),
            new Vector3(-6.5f, 0.5f, 0f),
            new Vector3(-6.5f, -0.5f, 0f),
            new Vector3(-6.5f, -1.5f, 0f),
            new Vector3(-6.5f, -2.5f, 0f),
            new Vector3(-6.5f, -3.5f, 0f),
            new Vector3(-6.5f, -4.5f, 0f)
        };
        private List<Vector3> leftDirections = new List<Vector3>()
        {
            new Vector3(6.5f, 4.5f, 0f),
            new Vector3(6.5f, 3.5f, 0f),
            new Vector3(6.5f, 2.5f, 0f),
            new Vector3(6.5f, 1.5f, 0f),
            new Vector3(6.5f, 0.5f, 0f),
            new Vector3(6.5f, -0.5f, 0f),
            new Vector3(6.5f, -1.5f, 0f),
            new Vector3(6.5f, -2.5f, 0f),
            new Vector3(6.5f, -3.5f, 0f),
            new Vector3(6.5f, -4.5f, 0f)
        };

        public Saucer SpawnSaucer(SaucerSize size)
        {
            //this.size = size;

            Vector3 location = GetRandomStartLocation();

            Saucer s = Instantiate<Saucer>(saucerPrefab, location, Quaternion.identity);
            switch (size)
            {
                case SaucerSize.Small:
                    s.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;
                case SaucerSize.Large:
                    s.transform.localScale = Vector3.one;
                    break;
                default:
                    s.transform.localScale = Vector3.one; // Default size
                    break;
            }
            LaunchSaucer(s.GetComponent<Rigidbody2D>(), Mathf.Sign(location.x) * -1.0f);
            s.SetSize(size);
            return s;
        }
        private void LaunchSaucer(Rigidbody2D rb, float sign)
        {
            rb.linearVelocity = Vector2.right * speed * sign; 
        }
        private Vector3 GetRandomStartLocation()
        {
            if (Random.value < 0.5f)
            {
                return rightDirections[Random.Range(0, rightDirections.Count)]; 

            } else
            {
                return leftDirections[Random.Range(0, leftDirections.Count)]; 
            }
        }
        public SaucerSize GetSize()
        {
            return size;
        }
        public void SetSize(SaucerSize size)
        {
            this.size = size;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == Layers.Player)
            {
                Debug.Log("Saucer::OnTriggerEnter2D Hit player; destroy it for now.");
                GameManager.Instance.PlayerDestroyed(other.gameObject);
            }
        }

        public void FireBullet(Transform playerLocation)
        {
            Debug.Log($"Saucer::FireBullet at {playerLocation.position}");
            // fire directly at player.  Not final design

            Vector3 directionToPlayer = playerLocation.position - cannon.transform.position;
            Vector3 forceDirection = (playerLocation.position - transform.position).normalized;

            EnemyBullet e = Instantiate<EnemyBullet>(bulletPrefab, cannon.transform.position, cannon.transform.rotation, null);
            e.Fire(forceDirection);
        }

    }


    public enum SaucerSize
    {
        Large, Small
    }
}
