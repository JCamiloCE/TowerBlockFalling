using JCC.Utils.GameplayEventSystem;
using JCC.Utils.Pool;
using Scripts.Blocks;
using Scripts.Building;
using Scripts.GameplayEvents;
using UnityEngine;

namespace Scripts.Crane 
{
    public class CraneController : MonoBehaviour, IEventListener<FinishFallingBlockEvent>
    {
        [SerializeField] private GameObject _initialBlock = null;
        [SerializeField] private BuildingController _buildingController = null;

        private IMovement _craneMovement = null;
        private IFallMovement _fallMovement = null;
        private IPoolController<BlockMonobehavior> _poolController = null;
        private BlockMonobehavior _currentBlock = null;

        #region IEventListener
        public void OnEvent(FinishFallingBlockEvent event_data)
        {
            SetNewBlockToTheCrane();
        }
        #endregion

        #region private
        private void Start()
        {
            EventManager.AddListener(this);
            CreatePoolForBlocks();
            ConfigureCraneMovement();
            ConfigureFallMovement();
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

        private void ConfigureCraneMovement() 
        {
            _craneMovement = GetComponent<IMovement>();
            _craneMovement.Initialization();
            _craneMovement.SetNewChildToMove(_currentBlock.transform);
            _craneMovement.StartMovement();
        }

        private void ConfigureFallMovement() 
        {
            _fallMovement = GetComponent<IFallMovement>();
            _fallMovement.Initialization();
        }

        private void StartFalling() 
        {
            _fallMovement.StartFalling(_currentBlock.transform);
            _currentBlock = null;
        }

        private void SetNewBlockToTheCrane() 
        {
            _currentBlock = _poolController.GetPoolObject();
            _craneMovement.SetNewChildToMove(_currentBlock.transform);
        }
        #endregion private
    }
}

