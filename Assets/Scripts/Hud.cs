using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Text score;
        [SerializeField] private Text highScore;
        [SerializeField] private Image[] lifeIcons;
        [SerializeField] private RectTransform shieldScale;
        [SerializeField] private CanvasGroup gameOverGroup;
        [SerializeField] private GameObject tutorial;

        private float _fullShieldDuration;
        
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
            highScore.text = GameManager.Instance.HighScore.ToString();
            _fullShieldDuration = GameSettings.Settings.ShipFullShieldDuration;

            StartCoroutine(ShowTutorialCoroutine());
        }

        private void Update()
        {
            RefreshShieldScale();
        }

        private void OnScoreChanged(object _)
        {
            score.text = GameManager.Instance.Score.ToString();
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
            while (gameOverGroup.alpha < 1 && gameOverGroup.transform.localScale.x < 1)
            {
                gameOverGroup.alpha += Time.deltaTime;
                gameOverGroup.transform.localScale += Time.deltaTime * Vector3.one;
                yield return null;
            }
        }
        
        private IEnumerator ShowTutorialCoroutine()
        {
            Time.timeScale = 0f;
            tutorial.SetActive(true);
            yield return new WaitUntil(() => Input.anyKeyDown);
            tutorial.SetActive(false);
            Time.timeScale = 1f;
        }

        private void RefreshShieldScale()
        {
            var newScale = shieldScale.localScale;
            
            float x = GameManager.Instance.Shield / _fullShieldDuration;
            if(x > 0.99999f)
                newScale.x = Mathf.Clamp(newScale.x + Time.deltaTime, 0, x);
            else 
                newScale = new Vector3(x, newScale.y, newScale.z);
            
            shieldScale.localScale = newScale;
        }

        private async void RefreshLives()
        {
            await Task.Yield();
            
            int lives = GameManager.Instance.Lives;
            for (int i = 0; i < lifeIcons.Length; i++)
            {
                lifeIcons[i].gameObject.SetActive(i < lives);
            }
        }
    }
}
