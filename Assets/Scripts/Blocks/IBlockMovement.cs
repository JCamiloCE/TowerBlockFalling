using JCC.Utils.LifeCycle;
using UnityEngine;

namespace Emc2.Scripts.Blocks
{ 
    public interface IBlockMovement : ILifeCycle
    {
        public void StartMovement();
        public void StopMovement();
        public void SetNewChildToMove(Transform newChildToMove);
    }
}

