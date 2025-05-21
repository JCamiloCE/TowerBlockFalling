using JCC.Utils.LifeCycle;
using UnityEngine;

namespace Emc2.Scripts.Blocks
{
    public interface IBlockFallMovement : ILifeCycle
    {
        public void StartFalling(Transform newFallingObject);
    }
}