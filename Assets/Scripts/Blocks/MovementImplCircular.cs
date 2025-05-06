using UnityEngine;

namespace Scripts.Blocks
{
    public class MovementImplCircular : MonoBehaviour, IMovement
    {
        [SerializeField] private float _radiusX = 3f;        
        [SerializeField] private float _radiusY = 3f;        
        [SerializeField] private float _angularSpeed = 60f;
        [SerializeField] private Transform _pivotPoint;     
        [SerializeField] private Transform _blockToMove = null;
        [SerializeField] private Transform _hook;
        [SerializeField] private LineRenderer _lineRenderer = null;

        private float _currentAngle = 0f;
        private bool _isMoving = false;

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
                /*currentAngle += _angularSpeed * Time.deltaTime;
                if (currentAngle >= 360f) currentAngle -= 360f;

                float radians = currentAngle * Mathf.Deg2Rad;

                Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * _radius;
                _blockToMove.position = _pivotPoint.position + offset;*/

                _currentAngle += _angularSpeed * Time.deltaTime;

                float x = Mathf.Cos(_currentAngle) * _radiusX;
                float y = Mathf.Sin(_currentAngle) * _radiusY;

                _blockToMove.position = _pivotPoint.position + new Vector3(x, -y, 0);

                _lineRenderer.SetPosition(0, _hook.position);
                _lineRenderer.SetPosition(1, _blockToMove.position);
            }
        }
        #endregion private
    }
}