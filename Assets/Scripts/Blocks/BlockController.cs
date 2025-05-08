using UnityEngine;

namespace Scripts.Blocks 
{
    public class BlockController : MonoBehaviour
    {
        private IMovement _blockMovement;

        private void Start()
        {
            _blockMovement = GetComponent<IMovement>();
            _blockMovement.Initialization();
            _blockMovement.StartMovement();
        }
    }
}

