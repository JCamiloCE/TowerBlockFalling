using UnityEngine;

namespace Script.Camera 
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _objectToFollow;
        [SerializeField] private float _deltaY;

        private float _lastYPosition;

        #region private
        void Update()
        {
            
            if (!Mathf.Approximately(_objectToFollow.position.y, _lastYPosition))
            {
                transform.position = new Vector3(transform.position.x, _objectToFollow.position.y + _deltaY, transform.position.z);
            }
            _lastYPosition = _objectToFollow.position.y;
        }
        #endregion private
    }
}
