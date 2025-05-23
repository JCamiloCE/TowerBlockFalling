using JCC.Utils.GameplayEventSystem;
using JCC.Utils.Pool;
using Emc2.Scripts.Blocks;
using Emc2.Scripts.GameplayEvents;
using UnityEngine;
using Emc2.Scripts.Building;
using System.Collections.Generic;

namespace Emc2.Scripts.Crane 
{
    public class CraneController : MonoBehaviour, IEventListener<FinishFallingBlockEvent>
    {
        [SerializeField] private GameObject _initialBlock = null; 
        [SerializeField] private BuildingController _buildingController = null;

        private IBlockMovement _blockMovement = null;
        private IBlockFallMovement _fallMovement = null;
        private ICraneMovement _craneMovement = null;
        private IPoolController<BlockMonobehavior> _poolController = null;
        private List<BlockMonobehavior> _usedBlocks = new List<BlockMonobehavior>();
        private List<BlockMonobehavior> _failedBlocks = new List<BlockMonobehavior>();
        private BlockMonobehavior _currentBlock = null;
        private int _currentBlockIndex = 0;
        private bool _isFalling = false;

        #region IEventListener
        public void OnEvent(FinishFallingBlockEvent event_data)
        {
            if (event_data.correctFalling)
            {
                RefreshUsedBlocks();
                _currentBlockIndex++;
                if (_currentBlockIndex > 2)
                {
                    _craneMovement.MoveUp();
                    return;
                }
            }
            else 
            {
                RefreshFailedBlocks();
            }
            SetNewBlockToTheCrane();
        }
        #endregion

        #region private
        private void Start()
        {
            EventManager.AddListener(this);
            CreatePoolForBlocks();
            ConfigureBlockMovement();
            ConfigureFallMovement();
            ConfigureCraneMovement();
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(this);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_isFalling)
            {
                StartFalling();
            }
        }

        private void CreatePoolForBlocks() 
        {
            _poolController = new PoolControllerImpl<BlockMonobehavior>();
            _poolController.SetPoolObject(_initialBlock, 10, true);
            _initialBlock.SetActive(false);
            _currentBlock = _poolController.GetPoolObject();
        }

        private void ConfigureBlockMovement() 
        {
            _blockMovement = GetComponent<IBlockMovement>();
            _blockMovement.Initialization();
            _blockMovement.SetNewChildToMove(_currentBlock.transform);
            _blockMovement.StartMovement();
        }

        private void ConfigureFallMovement() 
        {
            _fallMovement = GetComponent<IBlockFallMovement>();
            _fallMovement.Initialization();
        }

        private void ConfigureCraneMovement() 
        {
            _craneMovement = GetComponent<ICraneMovement>();
            _craneMovement.Initialization(transform);
            _craneMovement.SetCallBackEndMovement(SetNewBlockToTheCrane);
        }

        private void StartFalling() 
        {
            _isFalling = true;
            _fallMovement.StartFalling(_currentBlock.transform);
        }

        private void SetNewBlockToTheCrane() 
        {
            _currentBlock = _poolController.GetPoolObject();
            _blockMovement.SetNewChildToMove(_currentBlock.transform);
            _isFalling = false;
        }

        private void RefreshUsedBlocks()
        {
            _currentBlock.transform.SetParent(_buildingController.GetTransformToRotate());
            _currentBlock.transform.rotation = Quaternion.identity;
            _usedBlocks.Add(_currentBlock);
            if (_usedBlocks.Count > 5)
            {
                BlockMonobehavior currentBlock = _usedBlocks[0];
                _usedBlocks.RemoveAt(0);
                _poolController.ReturnToPool(currentBlock);
                _buildingController.RefreshRotationPosition();
            }
        }

        private void RefreshFailedBlocks()
        {
            _failedBlocks.Add(_currentBlock);
            if (_failedBlocks.Count > 5)
            {
                BlockMonobehavior currentBlock = _failedBlocks[0];
                _failedBlocks.RemoveAt(0);
                _poolController.ReturnToPool(currentBlock);
            }
        }
        #endregion private
    }
}

