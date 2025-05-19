using JCC.Utils.LifeCycle;
using UnityEngine;

namespace Scripts.Blocks
{
    public interface IBlockFallMovement : ILifeCycle
    {
        public void StartFalling(Transform newFallingObject);
    }
}