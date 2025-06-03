using Emc2.Scripts.GameplayEvents;
using JCC.Utils.GameplayEventSystem;
using JCC.Utils.Pool;
using JCC.Utils.Random;
using System.Collections.Generic;
using UnityEngine;

namespace Emc2.Scripts.BuildingTenant
{
    public class BuildingPeopleFalling : MonoBehaviour, IEventListener<FinishFallingBlockEvent>
    {
        [SerializeField] private GameObject _npcPrefab;
        [SerializeField] private List<Transform> _spawnPoints;

        private PoolControllerImpl<TenantNpc> _poolController = null;
        private IRandom _random;

        #region IEventListener
        public void OnEvent(FinishFallingBlockEvent event_data)
        {
            if (event_data.perfectFalling)
            {
                var startPos = _spawnPoints[0].position;
                TenantNpc tenantNpc = _poolController.GetPoolObject();
                tenantNpc.StartMovement(startPos, event_data.newBlock, () => _poolController.ReturnToPool(tenantNpc));

                var startPos2 = _spawnPoints[1].position;
                TenantNpc tenantNpc2 = _poolController.GetPoolObject();
                tenantNpc2.StartMovement(startPos2, event_data.newBlock, () => _poolController.ReturnToPool(tenantNpc2));
            }
            else if (event_data.correctFalling)
            {
                var startPos = _spawnPoints[_random.GetRandomIndexInList(_spawnPoints)].position;
                TenantNpc tenantNpc = _poolController.GetPoolObject();
                tenantNpc.StartMovement(startPos, event_data.newBlock, () => _poolController.ReturnToPool(tenantNpc));
            }
        }
        #endregion

        #region private
        private void Start()
        {
            EventManager.AddListener(this);
            ConfigurePool();
            _random = new RandomUnity();
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(this);
        }

        private void ConfigurePool() 
        {
            _poolController = new PoolControllerImpl<TenantNpc>();
            _poolController.SetPoolObject(_npcPrefab, 10, true);
        }
        #endregion
    }
}