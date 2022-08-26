using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace Asteroids
{
    public class LevelManager : MonoBehaviour
    {
        
        #region data 
        
        [SerializeField] private GameObject[] _levelsPrefabs;

        private GameObject _currentLevelObj; 
        
        private int _currentLevel = -1;
        private int _obstaclesCount = 0;

        #endregion
        
        #region implentation
        
        private void Awake()
        {
            EventManager.StartListening("OBSTACLE_SPAWNED", OnObstacleSpawned);
            EventManager.StartListening("OBSTACLE_DESTROYED", OnObstacleDestroyed);
        }

        private void Start()
        {
            StartNextLevel();
        }

        private void OnObstacleSpawned(object _)
        {
            _obstaclesCount++;
        }

        private void OnObstacleDestroyed(object _)
        {
            _obstaclesCount--;
            Assert.IsTrue(_obstaclesCount >= 0);

            if (_obstaclesCount == 0)
            {
                StartNextLevel();
            }
        }

        private void StartNextLevel()
        {
            if(_currentLevelObj != null)
                Destroy(_currentLevelObj);
            
            if (++_currentLevel >= _levelsPrefabs.Length)
            {
                _currentLevel = 0;
            }

            _currentLevelObj = Instantiate(_levelsPrefabs[_currentLevel], transform);
        }
        
        #endregion
        
    }
}