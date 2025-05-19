using JCC.Utils.DebugManager;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Crane
{
    public class CraneMovementImpl : MonoBehaviour, ICraneMovement
    {
        [SerializeField] private float _deltaToMove = 1f;
        [SerializeField] private float _moveTime = 1f;

        private bool _wasInitialized = false;
        private Coroutine _moveCoroutine = null;
        private Transform _craneToMove = null;
        private Action _cbEndMovement = null;

        #region ICraneMovement
        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            _craneToMove = parameters[0] as Transform;
            _wasInitialized = true;
            return _wasInitialized;
        }

        public void MoveUp()
        {
            if (!WasInitialized()) 
            {
                DebugManager.LogError("CraneMovementImpl::MoveUp -> Was not initialized");
                return;
            }
            StopMovementToUp();
            _moveCoroutine = StartCoroutine(MoveCraneToUp());
        }

        public void SetCallBackEndMovement(Action cb)
        {
            _cbEndMovement = cb;
        }
        #endregion ICraneMovement

        #region private 
        private void StopMovementToUp()
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
            _moveCoroutine = null;
        }

        private IEnumerator MoveCraneToUp() 
        {
            Vector3 currentTarget = new Vector3(_craneToMove.position.x, _craneToMove.position.y + _deltaToMove, _craneToMove.position.z);
            Vector3 startPos = _craneToMove.position;
            float currentTime = 0f;
            while (currentTime < _moveTime)
            {
                Vector3 newPos = Vector3.Lerp(startPos, currentTarget, currentTime / _moveTime);
                _craneToMove.position = newPos;
                currentTime += Time.deltaTime;
                yield return null;
            }
            _craneToMove.position = currentTarget;
            _cbEndMovement?.Invoke();
            _moveCoroutine = null;
        }
        #endregion private
    }
}