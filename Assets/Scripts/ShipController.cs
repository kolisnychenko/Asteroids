using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Asteroids
{
    public class ShipController : MonoBehaviour, ITagwiseCollisionReceiver
    {

        #region data

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject shield;
        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private Rigidbody2D bulletPrefabRb2D;

        private bool _isInvincible;
        private bool _isRespawning;

        #endregion

        #region interface

        public bool IsInvincible
        {
            get => _isInvincible;
            private set
            {
                shield.SetActive(value);
                _isInvincible = value;
            }
        }

        public void Respawn()
        {
            StartCoroutine(RespawnCor());
        }

        #endregion

        #region implementation

        private void Update()
        {
            HandleRotation();
            HandleFire();
            HandleShield();
        }

        private void FixedUpdate()
        {
            HandleThrust();
        }

        public void OnTagwiseCollisionEnter(Collision2D col)
        {
            if (!IsInvincible)
            {
                EventManager.EmitEvent("SHIP_DESTROYED", null);
            }
        }

        private void HandleRotation()
        {
            float rotationAxis = Input.GetAxis("Horizontal");
            if (rotationAxis != 0)
            {
                float rotationAmount = GameSettings.Settings.ShipRotatePerSec * Time.deltaTime;
                if (rotationAxis < 0)
                {
                    rotationAmount *= -1;
                }
                
                transform.Rotate(Vector3.forward * rotationAmount);
            }
        }        
        
        private void HandleFire()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var bulletRb2D = Instantiate(bulletPrefabRb2D, transform.position, transform.rotation);
                bulletRb2D.AddForce(GetShipDirectionVector() * GameSettings.Settings.ShipBulletImpulse);
            }
        }        
        
        private void HandleShield()
        {
            if(_isRespawning) return;
            
            if (Input.GetKey(KeyCode.S))
            {
                if (GameManager.Instance.Shield > 0)
                {
                    GameManager.Instance.Shield -= Time.deltaTime;
                    IsInvincible = true;
                    return;
                }
            }

            IsInvincible = false;
            
        }

        private void HandleThrust()
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                rb2D.AddForce(GetShipDirectionVector() * GameSettings.Settings.ShipThrustForce, ForceMode2D.Force);
            }
        }
        
        private IEnumerator RespawnCor()
        {
            _isRespawning = true;
            IsInvincible = true;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.5f);
                spriteRenderer.color = Color.clear;
                yield return new WaitForSeconds(0.5f);
                spriteRenderer.color = Color.white;
            }
            _isRespawning = false;
        }

        private Vector2 GetShipDirectionVector()
        {
            float zAxisRotation = transform.eulerAngles.z * Mathf.Deg2Rad + Mathf.PI/2;
            return new Vector2(Mathf.Cos(zAxisRotation), Mathf.Sin(zAxisRotation));
        }

        #endregion
        
    }
}
