using UnityEngine;

namespace Asteroids
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Shooting SFX")]
        [SerializeField] private AudioClip shootingClip;
        [SerializeField][Range(0, 1f)] private float shootingVolume = 0.5f;   //0 to 1

        [Header("Thrusting SFX")]
        [SerializeField] private AudioClip thrustingClip;
        [SerializeField][Range(0, 1f)] private float thrustingVolume = 0.5f;   //0 to 1

        [Header("Saucer Shooting SFX")]
        [SerializeField] private AudioClip saucerShootingClip;
        [SerializeField][Range(0, 1f)] private float saucerShootingVolume = 0.5f;   //0 to 1


        public void PlayShootingSFX()
        {
            if (shootingClip == null) return;
            AudioSource.PlayClipAtPoint(shootingClip,
                Camera.main.transform.position,
                shootingVolume);
        }

        public void PlayThrustingSFX()
        {
            if (thrustingClip == null) return;
            AudioSource.PlayClipAtPoint(thrustingClip, 
                Camera.main.transform.position, 
                thrustingVolume);
        }
        public void PlaySaucerShootingSFX()
        {
            if (thrustingClip == null) return;
            AudioSource.PlayClipAtPoint(saucerShootingClip, 
                Camera.main.transform.position, 
                saucerShootingVolume);
        }



        
    }
}
