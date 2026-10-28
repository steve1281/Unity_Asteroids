using TMPro;
using UnityEngine;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Score;
        public int score = 0;
        [SerializeField] private TextMeshProUGUI Ships;
        public int ships = 4;
        private string shipReference = "^^^^^^^^^^^^";

        private static GameManager instance;
        public static GameManager Instance { get => instance; private set => instance = value; }
        private void Awake()
        {
            if (Instance != null && Instance !=this) 
            { 
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            Score.text = score.ToString("000000000");
            Ships.text = shipReference[^ships..];
        }

        public void UpdateScore(int amount = 1)
        {
            score += amount;
        }
        public void UpdateShips(int amount = 1)
        {
            ships += amount;
        }
    }
}
