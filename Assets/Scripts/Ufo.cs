using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class Ufo : Obstacle
    {
        [SerializeField] private Rigidbody2D bulletPrefabRb2D;
        
        public override int ObstacleRewardPoints => GameSettings.Settings.UfoRewardPoints;

        private void Start()
        {
            StartCoroutine(FirePeriodically());
            StartCoroutine(MoveRandomly());
        }

        public override void OnTagwiseCollisionEnter(Collision2D col)
        {
            Destroy(gameObject);
        }

        private IEnumerator FirePeriodically()
        {
            while (true)
            {   
                yield return new WaitForSeconds(GameSettings.Settings.UfoReloadTime);
            
                var bulletRb2D = Instantiate(bulletPrefabRb2D, transform.position, Quaternion.identity);

                float randomAngle = Random.Range(0, 2 * Mathf.PI);
                bulletRb2D.transform.eulerAngles = new Vector3(0, 0, randomAngle * Mathf.Rad2Deg);
                bulletRb2D.AddForce(GetVectorFromAngle(randomAngle) * GameSettings.Settings.UfoBulletImpulse);
            }
        }

        private IEnumerator MoveRandomly()
        {
            while (true)
            {
                rb2D.velocity = GetStartDirectionVector() * GameSettings.Settings.UfoVelocity;
                
                yield return new WaitForSeconds(GameSettings.Settings.UfoChangeDirectionTime);
            }
        }
    }
}