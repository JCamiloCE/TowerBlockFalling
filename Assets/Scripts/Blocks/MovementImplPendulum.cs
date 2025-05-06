using UnityEngine;

namespace Scripts.Blocks
{
    public class MovementImplPendulum : MonoBehaviour, IMovement
    {
        [SerializeField] private float _swingSpeed = 1.5f;
        [SerializeField] private float _swingOscillation = 20f;
        [SerializeField] private float _swingRadius = 3f;
        [SerializeField] private Transform _pivotPoint = null;
        [SerializeField] private Transform _blockToMove = null;
        [SerializeField] private LineRenderer _lineRenderer = null;

        private bool _isMoving = false;
        private float _angle = 0;

        #region IMovement
        public void StartMovement()
        {
            _lineRenderer.positionCount = 2;
            _isMoving = true;
        }

        public void StopMovement()
        {
            _isMoving = false;
        }
        #endregion IMovement

        #region private
        private void Update()
        {
            if (_isMoving) 
            {
                _angle = Mathf.Sin(Time.time * _swingSpeed) * _swingOscillation;
                float radians = _angle * Mathf.Deg2Rad;

                Vector3 offset = new Vector3(Mathf.Sin(radians), -Mathf.Cos(radians), 0) * _swingRadius;
                _blockToMove.position = _pivotPoint.position + offset;

                _lineRenderer.SetPosition(0, _pivotPoint.position);
                _lineRenderer.SetPosition(1, _blockToMove.position);
            }
        }
        #endregion private
    }
}