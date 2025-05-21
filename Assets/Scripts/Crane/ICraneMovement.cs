using JCC.Utils.LifeCycle;
using System;

namespace Emc2.Scripts.Crane
{
    public interface ICraneMovement : ILifeCycle
    {
        public void MoveUp();
        public void SetCallBackEndMovement(Action cb);
    }
}