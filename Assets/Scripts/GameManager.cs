using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {

        #region data
        
        [SerializeField] private ShipController _shipController;
        
        private float _shield;
        
        private const string HighScoreKey = "HighScoreKey";
        
        #endregion

        #region interface
        
        public static GameManager Instance { get; set; }
        public int Score { get; private set; } = 0;
        public int HighScore { get; private set; } = 0;
        public int Lives { get; private set; }

        public float Shield
        {
            get => _shield;
            set => _shield = Mathf.Clamp(value, 0, GameSettings.Settings.ShipFullShieldDuration);
        }
        
        #endregion
        
        #region implementation

        private void Awake()
        {
            Instance = this;
            
            Initialize();
            
            EventManager.StartListening("OBSTACLE_DESTROYED", OnScoreChanged);
            EventManager.StartListening("SHIP_DESTROYED", OnShipDestroyed);
        }

        private void Initialize()
        {
            Lives = GameSettings.Settings.ShipLives;
            Shield = GameSettings.Settings.ShipFullShieldDuration;

            HighScore = PlayerPrefs.GetInt(HighScoreKey);
        }

        private void OnScoreChanged(object points)
        {
            Score += (int)points;
        }
        
        private void OnShipDestroyed(object _)
        {
            if (Lives > 0)
            {   
                Lives--;
                StartCoroutine(RespawnShipCor());
            }
            else
            {
                StartCoroutine(GameOverSequence());
            }
        }

        private IEnumerator RespawnShipCor()
        {
            Shield = GameSettings.Settings.ShipFullShieldDuration;
            
            _shipController.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            
            _shipController.gameObject.SetActive(true);
            _shipController.Respawn();
        }

        private IEnumerator GameOverSequence()
        {
            if (Score > HighScore)
            {
                PlayerPrefs.SetInt(HighScoreKey, Score);
            }

            _shipController.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(2f);
            
            EventManager.EmitEvent("GAME_OVER", Score);
            yield return new WaitUntil(() => Input.anyKeyDown);

            SceneManager.LoadScene(0);
        }
        
        #endregion
        
    }
}