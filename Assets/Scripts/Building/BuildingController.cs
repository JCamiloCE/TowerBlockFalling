using UnityEngine;

namespace Scripts.Building
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private Transform _TargetPos;

        public Vector3 GetTargetPosition() => _TargetPos.position;
    }
}