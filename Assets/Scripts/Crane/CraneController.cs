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
        private BlockMonobehavior _currentBlock = null;
        private int _currentBlockIndex = 0;

        #region IEventListener
        public void OnEvent(FinishFallingBlockEvent event_data)
        {
            if (event_data.correctFalling) 
            {
                _currentBlockIndex++;
                if (_currentBlockIndex > 2)
                {
                    _craneMovement.MoveUp();
                    return;
                }
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
            if (Input.GetMouseButtonDown(0) && _currentBlock != null)
            {
                StartFalling();
            }
        }

        private void CreatePoolForBlocks() 
        {
            _poolController = new PoolControllerImpl<BlockMonobehavior>();
            _poolController.SetPoolObject(_initialBlock, 20, true);
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
            RefreshUsedBlocks();
            _fallMovement.StartFalling(_currentBlock.transform);
            _currentBlock = null;
        }

        private void RefreshUsedBlocks() 
        {
            _currentBlock.transform.SetParent(_buildingController.GetTransformToRotate());
            _currentBlock.transform.rotation = Quaternion.identity;
            _usedBlocks.Add(_currentBlock);
            if (_usedBlocks.Count > 5) 
            {
                _usedBlocks.RemoveAt(0);
            }
        }

        private void SetNewBlockToTheCrane() 
        {
            _currentBlock = _poolController.GetPoolObject();
            _blockMovement.SetNewChildToMove(_currentBlock.transform);
        }
        #endregion private
    }
}

