using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Saucer : MonoBehaviour
    {
        [SerializeField] private Saucer saucerPrefab;
        private SaucerSize size; 
        public void SpawnSaucer(SaucerSize size, Vector3 location)
        {
            this.size = size;
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
        }
        public SaucerSize GetSize()
        {
            return size;
        }           
    }


    public enum SaucerSize
    {
        Large, Small
    }
}
