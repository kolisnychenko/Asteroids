using System.Collections;
using UnityEngine;

namespace Asteroids
{
    public class TimedObstacle : MonoBehaviour
    {
        [SerializeField] private float _waitBeforeEnableSec = 0f;
        [SerializeField] private Obstacle _obstacle;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_waitBeforeEnableSec);
            _obstacle.gameObject.SetActive(true);
        }
    }
}