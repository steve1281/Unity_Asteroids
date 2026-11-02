using UnityEngine;

namespace Asteroids
{
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private float newx=-99f;
        [SerializeField] private float newy=-99f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log($"Teleporter:::OnTriggerEnter called, modify X: {newx}, Y: {newy}");
            Vector3 newPosition = other.transform.position;
            newPosition.x = newx==-99f?newPosition.x:newx;
            newPosition.y = newy==-99f?newPosition.y:newy;
            other.transform.position = newPosition; 
        }
    }

}
