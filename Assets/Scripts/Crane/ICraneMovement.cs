using JCC.Utils.LifeCycle;
using System;

namespace Scripts.Crane
{
    public interface ICraneMovement : ILifeCycle
    {
        public void MoveUp();
        public void SetCallBackEndMovement(Action cb);
    }
}