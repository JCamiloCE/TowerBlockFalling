using JCC.Utils.GameplayEventSystem;
using Emc2.Scripts.GameplayEvents;
using UnityEngine;

namespace Emc2.Scripts.Building
{
    public class BuildingController : MonoBehaviour, IEventListener<FinishFallingBlockEvent>
    {
        [SerializeField] private Transform _targetPos = null;
        [SerializeField] private float _sizeBlock = 1f;

        public Vector3 GetTargetPosition() => _targetPos.position;

        #region IEventListener
        public void OnEvent(FinishFallingBlockEvent event_data)
        {
            if (event_data.correctFalling) 
            {
                _targetPos.position = new Vector3(event_data.newTargetPos.x, event_data.newTargetPos.y + _sizeBlock, event_data.newTargetPos.z);
            }
        }
        #endregion

        #region private
        private void Start()
        {
            EventManager.AddListener(this);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(this);
        }
        #endregion private
    }
}