using JCC.Utils.Pool;
using UnityEngine;

namespace Scripts.Blocks 
{
    public class BlocksController : MonoBehaviour
    {
        [SerializeField] private GameObject _initialBlock;

        private IMovement _blockMovement;
        private IFallMovement _fallMovement;
        private IPoolController<BlockMonobehavior> _poolController;

        private void Start()
        {
            _poolController = new PoolControllerImpl<BlockMonobehavior>();
            _poolController.SetPoolObject(_initialBlock, 20, true);

            _blockMovement = GetComponent<IMovement>();
            _blockMovement.Initialization();
            _blockMovement.StartMovement();

            _fallMovement = GetComponent<IFallMovement>();
            _fallMovement.Initialization();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartFalling();
            }
        }

        private void StartFalling() 
        {
            _fallMovement.StartFalling();
        }
    }
}

