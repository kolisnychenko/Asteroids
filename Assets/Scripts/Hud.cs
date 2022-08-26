using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Asteroids
{
    public class Hud : MonoBehaviour
    {
        
        #region data
        
        [SerializeField] private Text _score;
        [SerializeField] private Text _highScore;
        [SerializeField] private Image[] _lifeIcons;
        [SerializeField] private RectTransform _shieldScale;
        [SerializeField] private CanvasGroup _gameOverGroup;
        [SerializeField] private GameObject _tutorial;

        private float _fullShieldDuration;
        
        #endregion
        
        #region implementation
        
        private void Awake()
        {
            EventManager.StartListening("OBSTACLE_DESTROYED", OnScoreChanged);
            EventManager.StartListening("SHIP_DESTROYED", OnShipDestroyed);
            EventManager.StartListening("GAME_OVER", OnGameOver);
            EventManager.StartListening("GAME_OVER", OnScoreChanged);
        }

        private void Start()
        {
            RefreshLives();
            _highScore.text = GameManager.Instance.HighScore.ToString();
            _fullShieldDuration = GameSettings.Settings.ShipFullShieldDuration;

            StartCoroutine(ShowTutorialCoroutine());
        }

        private void Update()
        {
            RefreshShieldScale();
        }

        private void OnScoreChanged(object _)
        {
            _score.text = GameManager.Instance.Score.ToString();
        }

        private void OnShipDestroyed(object _)
        {
            RefreshLives();
        }

        private void OnGameOver(object _)
        {
            StartCoroutine(ShowGameOver());
        }

        private IEnumerator ShowGameOver()
        {
            while (_gameOverGroup.alpha < 1 && _gameOverGroup.transform.localScale.x < 1)
            {
                _gameOverGroup.alpha += Time.deltaTime;
                _gameOverGroup.transform.localScale += Time.deltaTime * Vector3.one;
                yield return null;
            }
        }
        
        private IEnumerator ShowTutorialCoroutine()
        {
            Time.timeScale = 0f;
            _tutorial.SetActive(true);
            yield return new WaitUntil(() => Input.anyKeyDown);
            _tutorial.SetActive(false);
            Time.timeScale = 1f;
        }

        private void RefreshShieldScale()
        {
            var newScale = _shieldScale.localScale;
            
            float x = GameManager.Instance.Shield / _fullShieldDuration;
            if(x > 0.99999f)
                newScale.x = Mathf.Clamp(newScale.x + Time.deltaTime, 0, x);
            else 
                newScale = new Vector3(x, newScale.y, newScale.z);
            
            _shieldScale.localScale = newScale;
        }

        private async void RefreshLives()
        {
            await Task.Yield();
            
            int lives = GameManager.Instance.Lives;
            for (int i = 0; i < _lifeIcons.Length; i++)
            {
                _lifeIcons[i].gameObject.SetActive(i < lives);
            }
        }
        
        #endregion
        
    }
}
