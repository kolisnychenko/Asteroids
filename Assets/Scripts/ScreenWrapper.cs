using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
    public class ScreenWrapper : MonoBehaviour
    {

        #region data

        [SerializeField] private Renderer _renderer;

        private bool _shouldWrap;

        #endregion
        
        #region interface

        public bool IsInsideMap =>
            ScreenUtils.ScreenBounds.Contains(transform.position) ||
            ScreenUtils.ScreenBounds.Intersects(_renderer.bounds);

        #endregion

        #region implementation

        private void Start()
        {
            _shouldWrap = IsInsideMap;
        }

        private void FixedUpdate()
        {
            if (!IsInsideMap && _shouldWrap)
            {
                WrapObject();
            }

            if (!_shouldWrap && IsInsideMap)
            {
                _shouldWrap = true;
            }
        }
        
        private void WrapObject()
        {
            var location = transform.position;
            
            if (location.x < ScreenUtils.ScreenBounds.min.x ||
                location.x > ScreenUtils.ScreenBounds.max.x)
            {
                location.x *= -1;
            }
            if (location.y > ScreenUtils.ScreenBounds.max.y ||
                location.y < ScreenUtils.ScreenBounds.min.y)
            {
                location.y *= -1;
            }
            
            transform.position = location;
            _shouldWrap = false;
        }

        private void OnDrawGizmos()
        {
            if(!Application.isPlaying) return;
            
            ScreenUtils.DrawBounds(ScreenUtils.ScreenBounds);
            ScreenUtils.DrawBounds(_renderer.bounds);
        }

        #endregion
        
    }
}
