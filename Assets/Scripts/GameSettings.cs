using UnityEngine;

namespace Asteroids
{
    public class GameSettings : MonoBehaviour
    {
        [SerializeField] private GameSettingsAsset asset;
        
        private static GameSettings Instance { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public static GameSettingsAsset Settings => Instance.asset;
    }
}