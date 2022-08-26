using System;
using System.Collections;
using UnityEngine;

namespace Asteroids
{
    public class TimedObstacle : MonoBehaviour
    {
        [SerializeField] private float waitBeforeEnableSec = 0f;
        [SerializeField] private Obstacle obstacle;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(waitBeforeEnableSec);
            obstacle.gameObject.SetActive(true);
        }
    }
}