using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ChameleonJump
{
    public class ChameleonJump : MonoBehaviour
    {
        public static ChameleonJump Instance;
        
        public Transform chameleonTransform;
        public ChameleonCollision chameleonCollision;
        public LaunchGameManager launchGameManager;

        [SerializeField] private Transform[] spawnPoints;
        public List<GameObject> currentObstacles;
        public GameObject enemyPrefab;

        [SerializeField] private float jumpPower = 5;
        [SerializeField] private float jumpDuration = 2;
        [SerializeField] private float timeToSpawn = 2;
        [SerializeField] private float xTarget = -1100;
        [SerializeField] private float timeToReachTarget = 10;

        private bool _airborne;

        private Tween _duck;
        private float _timer;

        private bool _started;

        private void Awake()
        {
            if (Instance != null && Instance != this) DestroyImmediate(gameObject);
            else Instance = this;
        }

        private void OnEnable()
        {
            _started = true;
            CameleonManager.instance.ChCamEnergy(-12);
            _timer = 0;
        }

        private void Update()
        {
            if (!_started) return;
            _timer += Time.deltaTime;

            if (_timer > timeToSpawn)
            {
                _timer = 0;
                SpawnObstacle();
            }
            
            if (Input.GetMouseButtonDown(1)) Duck();
            if (Input.GetMouseButtonUp(1)) DuckStop();
            
            if (_airborne) return;
            if(Input.GetMouseButtonDown(0)) Jump();
        }

        private void SpawnObstacle()
        {
            currentObstacles.Add(Instantiate(enemyPrefab,spawnPoints[Random.Range(0,spawnPoints.Length)].position,Quaternion.identity,transform));
            currentObstacles[^1].transform.DOMoveX(xTarget,timeToReachTarget);
        }
        
        private void Jump()
        {
            _airborne = true;
            
            chameleonTransform.DOJump(chameleonTransform.position,jumpPower,1, jumpDuration).OnComplete(() => _airborne = false);
        }
        
        private void Duck()
        {
            _duck.Kill();
            _duck = chameleonTransform.DOScaleY(.5f,.2f);
        }
        
        private void DuckStop()
        {
            _duck.Kill();
            _duck = chameleonTransform.DOScaleY(1,.2f);
        }

        public void LoseGame()
        {
            _started = false;
            
            foreach (var obstacle in currentObstacles)
            {
                Destroy(obstacle);
            }
            
            CameleonManager.instance.ChCamHealth(currentObstacles.Count);
            
            currentObstacles.Clear();
            
            launchGameManager.ReturnToMenuInGame();
        }
    }
}
