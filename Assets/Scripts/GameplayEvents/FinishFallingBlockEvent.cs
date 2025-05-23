using JCC.Utils.GameplayEventSystem;
using UnityEngine;

namespace Emc2.Scripts.GameplayEvents
{
    public class FinishFallingBlockEvent : EventBase
    {
        public bool correctFalling = false;
        public Vector3 newTargetPos = Vector3.zero;
        public float distanceToLastBlock = 0;

        public override void SetParameters(params object[] parameters) 
        {
            correctFalling = (bool)parameters[0];
            newTargetPos = (Vector3)parameters[1];
            distanceToLastBlock = (float)parameters[2];
        }
    }
}