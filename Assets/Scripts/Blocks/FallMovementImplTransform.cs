using JCC.Utils.DebugManager;
using JCC.Utils.GameplayEventSystem;
using Scripts.GameplayEvents;
using System.Collections;
using UnityEngine;

namespace Scripts.Blocks
{
    public class FallMovementImplTransform : MonoBehaviour, IFallMovement
    {
        [SerializeField] private float _fallingTime = 2f;
        [SerializeField] private float _deltaSize = 0.5f;
        [SerializeField] private Transform _target = null;

        private Transform _blockToMove = null;
        private bool _wasInitialized = false;
        private Coroutine _fallingMovement = null;

        #region IFallMovement
        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            _fallingMovement = null;
            _blockToMove = null;
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
            EventManager.TriggerEvent<FinishFallingBlockEvent>();
            _fallingMovement = null;
        }
        #endregion
    }
}