using JCC.Utils.DebugManager;
using JCC.Utils.LifeCycle;
using UnityEngine;

namespace Scripts.Blocks 
{
    public class BlockMovement : MonoBehaviour, ILifeCycle
    {
        private IMovement _movement;
        private bool _wasInitialized;

        #region ILifeCycle
        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            _wasInitialized = true;
            _wasInitialized = _wasInitialized && SetMovement(parameters[0] as IMovement);
            return _wasInitialized;
        }
        #endregion ILifeCycle

        #region public
        #endregion public

        #region internal
        internal bool SetMovement(IMovement movement)
        {
            if (movement == null)
            {
                DebugManager.LogError("BlockMovement.SetMovement :: movement is null");
                return false;
            }
            _movement = movement;
            return true;
        }

        internal void StartMovement() 
        {
            if (!HasValidConfiguration())
                return;
            _movement.StartMovement();
        }

        internal void StopMovement()
        {
            if (!HasValidConfiguration())
                return;
            _movement.StopMovement();
        }
        #endregion internal

        #region private
        private bool HasValidConfiguration() 
        {
            if (_movement == null)
            {
                DebugManager.LogError("BlockMovement.HasValidConfiguration :: movement is null");
                return false;
            }
            if (!WasInitialized()) 
            {
                DebugManager.LogError("BlockMovement.HasValidConfiguration :: was not initialized");
                return false;
            }
            return true;
        }
        #endregion private
    }
}
