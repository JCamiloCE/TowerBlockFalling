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
        private bool _wasInitialized = false;
        private Vector3 _previousPosition;

        public float minValue;
        public float maxValue;
        public float delta;

        #region IMovement
        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            SetInitialValues();
            _wasInitialized = true;
            return _wasInitialized;
        }
        public void StartMovement()
        {
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
                _blockToMove.position = GetNewPosition();

                Vector3 direction = _blockToMove.position - _previousPosition;
                float angleZ = Mathf.Clamp(-direction.x * delta, minValue, maxValue);

                _blockToMove.rotation = Quaternion.Euler(0, 0, angleZ);

                _previousPosition = _blockToMove.position;
                SetPositionsInLineRender();
            }
        }

        private Vector3 GetNewPosition() 
        {
            _currentAngle += _angularSpeed * Time.deltaTime;

            float x = Mathf.Cos(_currentAngle) * _radiusX;
            float y = Mathf.Sin(_currentAngle) * _radiusY;

            return _pivotPoint.position + new Vector3(x, -y, 0);
        }

        private void SetInitialValues() 
        {
            _lineRenderer.positionCount = 2;
            SetPositionsInLineRender();
            _isMoving = false;
        }

        private void SetPositionsInLineRender() 
        {
            _lineRenderer.SetPosition(0, _hook.position);
            _lineRenderer.SetPosition(1, _blockToMove.position);
        }
        #endregion private
    }
}