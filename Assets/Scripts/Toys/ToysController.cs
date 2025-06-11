using JCC.Utils.Pool;
using JCC.Utils.Random;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Emc2.Scripts.BuildingTenant
{
    public class ToysController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraPosition = null;
        [SerializeField] private ToyDataScriptableObject _toyScriptable = null;
        [SerializeField] private GameObject _prefabToy = null;
        [SerializeField] private float _timeForSpawn = 5f;
        [SerializeField] private List<Transform> _spawnPoints = null; 

        private IPoolController<ToyController> _poolController = null;
        private float _timeSinceLastSpawn;
        private IRandom _random;
        private bool _startInLeft;
        private Vector3 _initPos;
        private Vector3 _endPos;

        #region private
        private void Start()
        {
            _poolController = new PoolControllerImpl<ToyController>();
            _poolController.SetPoolObject(_prefabToy, 10, true);
            _random = new RandomUnity();
        }

        private void Update()
        {
            if (_timeSinceLastSpawn + _timeForSpawn < Time.time) 
            {
                ToyData toyData = _toyScriptable.GetToyDataByHeight(_cameraPosition.position.y);
                if (toyData != null) 
                {
                    _timeSinceLastSpawn = Time.time;
                    ToyController toyController = _poolController.GetPoolObject();
                    _startInLeft = _random.GetRandomIntBetween(0, 2) == 0 ? true : false;
                    _initPos = _spawnPoints[_startInLeft ? 0 : 1].position;
                    _initPos = new Vector3(_initPos.x, _cameraPosition.position.y, _initPos.z);
                    _endPos = _spawnPoints[_startInLeft ? 1 : 0].position;
                    _endPos = new Vector3(_endPos.x, _cameraPosition.position.y, _endPos.z);
                    toyController.InitMovement(toyData, _initPos, _endPos, ()=> _poolController.ReturnToPool(toyController));
                }
            }
        }
        #endregion
    }
}