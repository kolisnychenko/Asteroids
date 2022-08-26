using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public abstract class Obstacle : MonoBehaviour, ITagwiseCollisionReceiver
    {
        
        #region data
        
        [SerializeField] protected ScreenWrapper screenWrapper;
        [SerializeField] protected Rigidbody2D rb2D;

        #endregion
        
        #region interface
        
        public abstract int ObstacleRewardPoints { get; }
        
        public abstract void OnTagwiseCollisionEnter(Collision2D col);
        
        protected virtual void OnEnable()
        {
            EventManager.EmitEvent("OBSTACLE_SPAWNED", this);
        }

        protected virtual void OnDisable()
        {
            EventManager.EmitEvent("OBSTACLE_DESTROYED", ObstacleRewardPoints);
        }
        
        protected Vector2 GetStartDirectionVector()
        {
            if (!screenWrapper.IsInsideMap)
            {
                return ScreenUtils.GetVectorFacingBounds(transform.position);
            }

            return GetRandomDirectionVector();
        }
        
        protected static Vector2 GetRandomDirectionVector()
        {
            float angle = Random.Range(0, 2 * Mathf.PI);
            return GetVectorFromAngle(angle);
        }
        
        protected static Vector2 GetVectorFromAngle(float angle)
        {
            return new Vector2(
                Mathf.Cos(angle), Mathf.Sin(angle));  
        }
        
        #endregion
    }
}