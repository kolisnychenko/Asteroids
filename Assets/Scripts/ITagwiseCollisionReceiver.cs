using UnityEngine;

namespace Asteroids
{
    public interface ITagwiseCollisionReceiver
    {
        void OnTagwiseCollisionEnter(Collision2D col);
    }
}