using UnityEngine;

namespace Scripts.Blocks 
{
    public class BlockController : MonoBehaviour
    {
        private IMovement _blockMovement;
        private IFallMovement _fallMovement;

        private void Start()
        {
            _blockMovement = GetComponent<IMovement>();
            _blockMovement.Initialization();
            _blockMovement.StartMovement();

            _fallMovement = GetComponent<IFallMovement>();
            _fallMovement.Initialization();
        }

        public void StartFalling() 
        {
            _fallMovement.StartFalling();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartFalling();
            }
        }
    }
}

