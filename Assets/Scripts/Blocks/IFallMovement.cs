using JCC.Utils.LifeCycle;

namespace Scripts.Blocks
{
    public interface IFallMovement : ILifeCycle
    {
        public void StartFalling();
    }
}