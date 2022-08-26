using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
    public class TagwiseCollisionDetector : MonoBehaviour
    {
        public bool IgnoreSelf = true;
        public string[] TagsToIgnore;
        
        private ITagwiseCollisionReceiver[] _receivers;
        
        private void Awake()
        {
            _receivers = GetComponents<ITagwiseCollisionReceiver>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            string colTag = col.gameObject.tag;
            
            if (IgnoreSelf && tag.Equals(colTag))
                return;
            
            if (TagsToIgnore.Any(tagToIgnore => tagToIgnore.Equals(colTag)))
                return;
    
            foreach (var receiver in _receivers)
            {
                receiver.OnTagwiseCollisionEnter(col);
            }
        }
    }
}