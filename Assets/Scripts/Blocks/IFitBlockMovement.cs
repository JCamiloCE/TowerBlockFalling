using UnityEngine;

namespace Emc2.Scripts.Blocks
{
    public interface IFitBlockMovement
    {
        public void StartFitBlock(Transform block, Vector3 lastTargetPos);
    }
}