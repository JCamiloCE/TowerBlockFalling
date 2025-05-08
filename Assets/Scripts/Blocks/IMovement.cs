using JCC.Utils.LifeCycle;

namespace Scripts.Blocks
{ 
    public interface IMovement : ILifeCycle
    {
        public void StartMovement();
        public void StopMovement();
    }
}

