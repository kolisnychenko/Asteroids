using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "AsteroidsGameSettings", menuName = "ScriptableObjects/AsteroidsGameSettings")]
    public class GameSettingsAsset : ScriptableObject
    {
        public int ShipLives = 2;
        public float ShipFullShieldDuration = 5f;
        public float ShipRotatePerSec = 90f;
        public float ShipThrustForce = 10f;
        public float ShipBulletImpulse = 0.5f;
        public float ShipBulletLifeTime = 2f;

        public int AsteroidRewardPoints = 100;
        public float AsteroidMinImpulse = 1f;
        public float AsteroidMaxImpulse = 5f;
        public float AsteroidMaxTorque = 15f;
        public float AsteroidImpulseLossCoef = 0.5f;
        public int AsteroidSplitPieces = 2;
        public int AsteroidSplitTimes = 2;
        public float AsteroidSpitScaleFactor = 2f;

        public int UfoRewardPoints = 150;
        public float UfoReloadTime = 1.5f;
        public float UfoBulletImpulse = 12f;
        public float UfoChangeDirectionTime = 3f;
        public float UfoVelocity = 20f;

        public int DeathStarRewardPoints = 150;
        public float DeathStarVelocity = 1f;
        public float DeathStarRotation = 5f;
    }   
}