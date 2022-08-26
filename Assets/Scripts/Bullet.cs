using System;
using System.Collections;
using UnityEngine;

namespace Asteroids
{
    public class Bullet : MonoBehaviour, ITagwiseCollisionReceiver
    {
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(GameSettings.Settings.ShipBulletLifeTime);
            Destroy(gameObject);
        }

        public void OnTagwiseCollisionEnter(Collision2D col)
        {
            Destroy(gameObject);
        }
    }
}