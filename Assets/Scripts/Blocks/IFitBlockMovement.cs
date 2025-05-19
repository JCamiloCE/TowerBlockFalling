using UnityEngine;

namespace Scripts.Blocks
{
    public interface IFitBlockMovement
    {
        public void StartFitBlock(Transform block, Vector3 lastTargetPos);
    }
}