using JCC.Utils.GameplayEventSystem;
using Emc2.Scripts.GameplayEvents;
using UnityEngine;

namespace Emc2.Scripts.Building
{
    public class BuildingController : MonoBehaviour, IEventListener<FinishFallingBlockEvent>
    {
        [SerializeField] private Transform _targetPos = null;
        [SerializeField] private Transform _transformToRotate = null;
        [SerializeField] private float _sizeBlock = 1f;
        [SerializeField] private float _maxAngleToRotate = 2f;
        [SerializeField] private float _speed = 1f;

        private float _minAngle = 0f;
        private float _maxAngle = 0f;
        private int _currentBlock = 0;

        public Vector3 GetTargetPosition() => _targetPos.position;

        #region IEventListener
        public void OnEvent(FinishFallingBlockEvent event_data)
        {
            if (event_data.correctFalling) 
            {
                _currentBlock++;
                _targetPos.position = new Vector3(event_data.newTargetPos.x, event_data.newTargetPos.y + _sizeBlock, event_data.newTargetPos.z);
                AddBalance(event_data.distanceToLastBlock);
            }
        }
        #endregion

        #region public
        public Transform GetTransformToRotate() => _transformToRotate;
        #endregion public

        #region private
        private void Start()
        {
            EventManager.AddListener(this);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(this);
        }

        void Update()
        {
            float t = (Mathf.Sin(Time.time * _speed) + 1f) / 2f;
            float angle = Mathf.Lerp(_minAngle, _maxAngle, t);
            _transformToRotate.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private void AddBalance(float distance) 
        {
            if (_currentBlock > 3) 
            {
                if (distance > 0)
                {
                    _maxAngle += distance;
                    _maxAngle = Mathf.Clamp(_maxAngle, 0, _maxAngleToRotate);
                }
                else 
                {
                    _minAngle += distance;
                    _minAngle = Mathf.Clamp(_minAngle, -_maxAngleToRotate, 0);
                }
            }
        }
        #endregion private
    }
}