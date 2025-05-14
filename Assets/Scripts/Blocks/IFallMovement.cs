using JCC.Utils.LifeCycle;
using UnityEngine;

namespace Scripts.Blocks
{
    public interface IFallMovement : ILifeCycle
    {
        public void StartFalling(Transform newFallingObject);
    }
}