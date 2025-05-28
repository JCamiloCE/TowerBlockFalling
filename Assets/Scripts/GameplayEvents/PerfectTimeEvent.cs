using JCC.Utils.GameplayEventSystem;

namespace Emc2.Scripts.GameplayEvents
{
    public class PerfectTimeEvent : EventBase
    {
        public bool isStart;

        public override void SetParameters(params object[] parameters)
        {
            isStart = (bool)parameters[0];
        }
    }
}
