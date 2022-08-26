using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class Asteroid : Obstacle
    {
        private int TimesSplit { get; set; } = 0;
        
        public override int ObstacleRewardPoints => GameSettings.Settings.AsteroidRewardPoints;

        public override void OnTagwiseCollisionEnter(Collision2D col)
        {
            TimesSplit++;

            if (TimesSplit > GameSettings.Settings.AsteroidSplitTimes)
            {
                Destroy(gameObject);
            }
            else
            {
                ScaleCurrentAsteroid(GameSettings.Settings.AsteroidSpitScaleFactor);
                for (int i = 0; i < GameSettings.Settings.AsteroidSplitPieces; i++)
                {
                    var asteroid = Instantiate(this, transform.position, Quaternion.identity);
                    asteroid.TimesSplit = TimesSplit;
                }

                Destroy(gameObject);
            }
        }
        
        private void Start()    
        {
            rb2D.AddForce(GetStartDirectionVector() 
                * Mathf.Pow(GameSettings.Settings.AsteroidImpulseLossCoef, TimesSplit + 1)
                * Random.Range(GameSettings.Settings.AsteroidMinImpulse, GameSettings.Settings.AsteroidMaxImpulse),
                ForceMode2D.Impulse);
            rb2D.AddTorque(Mathf.Pow(GameSettings.Settings.AsteroidImpulseLossCoef, TimesSplit + 1) *
                Random.Range(-GameSettings.Settings.AsteroidMaxTorque, GameSettings.Settings.AsteroidMaxTorque));
        }

        private void ScaleCurrentAsteroid(float factor)
        {
            transform.localScale /= factor;
        }
    }
}