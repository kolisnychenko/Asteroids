using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
    public class TagwiseCollisionDetector : MonoBehaviour
    {
        public bool ignoreSelf = true;
        public string[] tagsToIgnore;
        
        private ITagwiseCollisionReceiver[] _receivers;
        
        private void Awake()
        {
            _receivers = GetComponents<ITagwiseCollisionReceiver>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            string colTag = col.gameObject.tag;
            
            if (ignoreSelf && tag.Equals(colTag))
                return;
            
            if (tagsToIgnore.Any(tagToIgnore => tagToIgnore.Equals(colTag)))
                return;
    
            foreach (var receiver in _receivers)
            {
                receiver.OnTagwiseCollisionEnter(col);
            }
        }
    }
}