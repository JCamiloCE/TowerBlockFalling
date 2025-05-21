using JCC.Utils.DebugManager;
using JCC.Utils.GameplayEventSystem;
using Emc2.Scripts.GameplayEvents;
using System.Collections;
using UnityEngine;

namespace Emc2.Scripts.Blocks
{
    public class FallMovementImplTransform : MonoBehaviour, IBlockFallMovement
    {
        [SerializeField] private float _fallingTime = 2f;
        [SerializeField] private float _deltaSize = 0.5f;
        [SerializeField] private float _distanceToFit = 0.5f;
        [SerializeField] private float _forceWhenNotFit = 5f;
        [SerializeField] private Transform _target = null;

        private Transform _blockToMove = null;
        private bool _wasInitialized = false;
        private Coroutine _fallingMovement = null;
        private Coroutine _deactivateBlock = null;
        private bool _firstInFall = true;

        #region IFallMovement
        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            _fallingMovement = null;
            _blockToMove = null;
            _firstInFall = true;
            _wasInitialized = true;
            return _wasInitialized;
        }

        public void StartFalling(Transform newFallingObject)
        {
            if (newFallingObject == null)
            {
                DebugManager.LogError("FallMovementImplTransform::StartFalling -> newFallingObject is null");
                return;
            }
            _blockToMove = newFallingObject;
            if (WasInitialized())
            { 
                StopFallingMovement();
                _fallingMovement = StartCoroutine(FallingMovement());
            }
        }
        #endregion IFallMovement

        #region private
        private void StopFallingMovement() 
        {
            if (_fallingMovement != null)
            {
                StopCoroutine(_fallingMovement);
            }
            _fallingMovement = null;
        }

        private IEnumerator FallingMovement() 
        {
            _blockToMove.SetParent(null);
            _blockToMove.rotation = Quaternion.identity;
            Vector3 currentTarget = new Vector3(_blockToMove.position.x, _target.position.y + _deltaSize, _target.position.z);
            Vector3 startPos = _blockToMove.position;
            float currentTime = 0f;
            while (currentTime < _fallingTime) 
            {
                Vector3 newPos = Vector3.Lerp(startPos, currentTarget, currentTime/_fallingTime);
                _blockToMove.position = newPos;
                currentTime += Time.deltaTime;
                yield return null;
            }
            _blockToMove.position = currentTarget;
            FitWithTheTarget();
            _firstInFall = false;
            _fallingMovement = null;
        }

        private void FitWithTheTarget() 
        {
            Vector3 targetPos = new Vector3(_target.position.x, _target.position.y + _deltaSize, _target.position.z);
            float currentDistance = Vector3.Distance(_blockToMove.position, targetPos);
            bool fit = currentDistance <= _distanceToFit;
            EventManager.TriggerEvent<FinishFallingBlockEvent>(fit, _blockToMove.position);
            if (_firstInFall == false && fit == false) 
            {
                ReleaseObject(currentDistance);
            }
        }

        private void ReleaseObject(float distance) 
        {
            Rigidbody rg = _blockToMove.GetComponent<Rigidbody>();
            rg.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            rg.useGravity = true;
            rg.AddForce(Vector3.down * _forceWhenNotFit * distance, ForceMode.Impulse);
            StopDeactivateBlock();
            _deactivateBlock = StartCoroutine(DeactivateBlock(_blockToMove));
        }

        private void StopDeactivateBlock()
        {
            if (_fallingMovement != null)
            {
                StopCoroutine(_fallingMovement);
            }
            _fallingMovement = null;
        }

        private IEnumerator DeactivateBlock(Transform block)
        {
            yield return new WaitForSeconds(1);
            block.gameObject.SetActive(false);
        }
        #endregion
    }
}