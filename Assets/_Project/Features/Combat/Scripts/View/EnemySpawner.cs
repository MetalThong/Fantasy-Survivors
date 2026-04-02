using System.Collections;
using System.Collections.Generic;
using Core.Foundation.Events;
using Reflex.Attributes;
using UnityEngine;

namespace Features.Combat
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyConfigSO _enemyConfig;
        [SerializeField] private EnemyEntityView _enemyViewPrefab;
        [SerializeField] private Transform _targetTransform; 

        private GameLoop _gameLoop;
        private EnemyEntityFactory _enemyFactory;
        private IEventBus _eventBus;


        [Inject]
        public void Construct(GameLoop gameLoop, EnemyEntityFactory enemyFactory, IEventBus eventBus)
        {
            _gameLoop = gameLoop;
            _enemyFactory = enemyFactory;
            _eventBus = eventBus;

            EnemyEntity enemy1 = SpawnEnemy();
            EnemyEntity enemy2 = SpawnEnemy();

            enemy1.SetTarget(enemy2);
            enemy1.SetCurrentPosition(new(-5, 0, 0));

            enemy2.SetTarget(enemy1);
            enemy2.SetCurrentPosition(new(5, 0, 0));
        }

        private EnemyEntity SpawnEnemy()
        {
            EnemyEntity enemy = _enemyFactory.Create(_enemyConfig);
            _gameLoop.Register(enemy);

            EnemyEntityView view = Instantiate(_enemyViewPrefab);
            view.Initialize(enemy, _eventBus);

            return enemy;
        }
    }
}