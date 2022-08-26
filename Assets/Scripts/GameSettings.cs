using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
    public class GameSettings : MonoBehaviour
    {
        private static GameSettings Instance { get; set; }

        [SerializeField] private GameSettingsAsset _asset;
        
        private void Awake()
        {
            Instance = this;
        }

        public static GameSettingsAsset Settings => Instance._asset;
    }
}