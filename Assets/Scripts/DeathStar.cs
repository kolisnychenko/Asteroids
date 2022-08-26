using System;
using System.Collections;
using UnityEngine;

namespace Asteroids
{
    public class DeathStar : Obstacle
    {
        [SerializeField] private DeathStar[] childDeathStars;

        private Transform _playerTransform;
        
        public override int ObstacleRewardPoints => GameSettings.Settings.DeathStarRewardPoints;
        
        public override void OnTagwiseCollisionEnter(Collision2D col)
        {
            ReleaseChildren();
            Destroy(gameObject);
        }

        private void Start()
        {
            StartCoroutine(FollowPlayer());
        }

        private IEnumerator FollowPlayer()
        {
            while (true)
            {
                if (!_playerTransform)
                {
                    _playerTransform = FindObjectOfType<ShipController>(true).transform;
                }

                var position = _playerTransform.position;
                var position1 = transform.position;
                float angle = Mathf.Atan2(position.y - position1.y,
                    position.x - position1.x);
                transform.eulerAngles = new Vector3(0, 0, (angle - Mathf.PI) * Mathf.Rad2Deg );
                rb2D.velocity = GetVectorFromAngle(angle) * GameSettings.Settings.DeathStarVelocity;
                yield return null;
            }
        }

        private void ReleaseChildren()
        {
            foreach (var deathStar in childDeathStars)
            {
                deathStar.transform.parent = transform.parent;
                deathStar.rb2D.simulated = true;
                deathStar.enabled = true;
            }
        }
    }
} 