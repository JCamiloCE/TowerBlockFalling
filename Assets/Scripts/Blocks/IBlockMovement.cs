using JCC.Utils.LifeCycle;
using UnityEngine;

namespace Scripts.Blocks
{ 
    public interface IBlockMovement : ILifeCycle
    {
        public void StartMovement();
        public void StopMovement();
        public void SetNewChildToMove(Transform newChildToMove);
    }
}

