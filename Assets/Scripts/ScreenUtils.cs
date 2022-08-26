using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public static class ScreenUtils
    {
        private static int _screenWidth;
        private static int _screenHeight;
        private static Bounds _screenBounds;
        
        public static Bounds ScreenBounds
        {
            get
            {
                CheckScreenSizeChanged();
                return _screenBounds;
            }
        }

        public static Vector2 GetVectorFacingBounds(Vector2 startPoint)
        {
            var bounds = ScreenBounds;
                
            var boundVertices = new List<Vector2>
            {
                bounds.min,
                new Vector2(bounds.min.x, bounds.max.y),
                new Vector2(bounds.max.x, bounds.min.y),
                bounds.max
            };

            boundVertices.Sort(((vec1, vec2) =>
            {
                float dist = Vector2.Distance(vec1, startPoint) - Vector2.Distance(vec1, startPoint);
                return dist > 0 ? 1 : -1;
            }));

            var randomDotOnOpposedDiagonal = Vector2.Lerp(boundVertices[1], boundVertices[2], Random.value);
            return (randomDotOnOpposedDiagonal - startPoint).normalized;
        }

        private static void Initialize()
        {
            _screenHeight = Screen.height;
            _screenWidth = Screen.width;
            
            var cameraBounds = Camera.main.ViewportToWorldPoint(Vector3.one) * 2;
            _screenBounds = new Bounds(Vector3.zero, new Vector2(cameraBounds.x, cameraBounds.y));
        }
        
        private static void CheckScreenSizeChanged()
        {
            if (_screenWidth != Screen.width || _screenHeight != Screen.height)
            {
                Initialize();
            }
        }

        public static void DrawBounds(Bounds b, float delay=0)
        {
            // bottom
            var p1 = new Vector3(b.min.x, b.min.y, b.min.z);
            var p2 = new Vector3(b.max.x, b.min.y, b.min.z);

            Debug.DrawLine(p1, p2, Color.red, delay);

            // top
            var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
            var p6 = new Vector3(b.max.x, b.max.y, b.min.z);

            Debug.DrawLine(p5, p6, Color.red, delay);

            // sides
            Debug.DrawLine(p1, p5, Color.red, delay);
            Debug.DrawLine(p2, p6, Color.red, delay);
        }
    }
}