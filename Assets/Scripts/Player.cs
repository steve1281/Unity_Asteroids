using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private float rotationSpeed = 100f;

        private void Update()
        {
            if (Keyboard.current.rightArrowKey.isPressed)
            {
                Debug.Log("Player::Update Rotate Right");
                player.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                Debug.Log("Player::Update Rotate Left");
                player.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }
            if (Keyboard.current.downArrowKey.isPressed)
            {
                Debug.Log("Player::Update Down Arrow Pressed");
            }
            if (Keyboard.current.upArrowKey.isPressed)
            {
                Debug.Log("Player::Update Thrust");
            }
            if (Keyboard.current.spaceKey.isPressed)
            {
                Debug.Log("Player::Update Hyperspace");
            }
        }
    }
}
