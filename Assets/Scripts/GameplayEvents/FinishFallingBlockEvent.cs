using JCC.Utils.GameplayEventSystem;
using UnityEngine;

namespace Scripts.GameplayEvents
{
    public class FinishFallingBlockEvent : EventBase
    {
        public bool correctFalling = false;
        public Vector3 newTargetPos = Vector3.zero;
        public override void SetParameters(params object[] parameters) 
        {
            correctFalling = (bool)parameters[0];
            newTargetPos = (Vector3)parameters[1];
        }
    }
}