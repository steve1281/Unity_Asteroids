using TMPro;
using UnityEngine;

namespace Asteroids
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Score;
        [SerializeField] private TextMeshProUGUI HighScore;
        [SerializeField] private TextMeshProUGUI Ships;
        [SerializeField] private GameObject gameOver;

        public void UpdateShips(int ships)
        {
            string shipReference = "^^^^^^^^^^^^";
            if (ships > -1)
            {
                Ships.text = shipReference[^ships..];
            }
        }

        public void UpdateScore(int score)
        {
            Score.text = score.ToString("000000000");
        }

        public void ToggleGameOver()
        {
            gameOver.SetActive(!gameOver.activeSelf);
        }
        public void UpdateHighScore(int highScore)
        {
            HighScore.text = highScore.ToString("000000000");
        }
    }
}
