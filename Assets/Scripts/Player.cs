using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private float rotationSpeed = 100f;

        [SerializeField] private GameObject thrust;
        [SerializeField] private GameObject sprite;

        [SerializeField] private float thrusterPower = 15f;

        [SerializeField] private GameObject cannon;
        [SerializeField] private GameObject bulletPrefab;


        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Keyboard.current.rightArrowKey.isPressed)
            {
                //Debug.Log("Player::Update Rotate Right");
                player.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                //Debug.Log("Player::Update Rotate Left");
                player.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }
            if (Keyboard.current.downArrowKey.isPressed)
            {
                Debug.Log("Player::Update Down Arrow Pressed");
            }
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                //Debug.Log("Player::Update Thrust");
                StartCoroutine(Thrust());
                rb.AddForce(transform.right * thrusterPower );

            }
            if (Keyboard.current.spaceKey.wasPressedThisFrame) //isPressed)
            {
                if (GameManager.Instance.CanPlayerFire())
                {
                    Instantiate(bulletPrefab, cannon.transform.position, cannon.transform.rotation, null);
                    GameManager.Instance.AddPlayerBullet();
                }
            }
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                GameManager.Instance.PlayerDestroyed(gameObject);
            }
            if (Keyboard.current.wKey.wasPressedThisFrame)
            {
                GameManager.Instance.FireSaucerBullet();
            }

            // bug fix: remove angular (spin) from the player
            rb.angularVelocity = 0f;
        }
        private IEnumerator Thrust()
        {
            sprite.SetActive(false);
            thrust.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            sprite.SetActive(true);
            thrust.SetActive(false);
        }
    }
}
