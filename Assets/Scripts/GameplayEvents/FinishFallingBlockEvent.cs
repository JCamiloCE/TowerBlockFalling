using JCC.Utils.GameplayEventSystem;
using UnityEngine;

namespace Emc2.Scripts.GameplayEvents
{
    public class FinishFallingBlockEvent : EventBase
    {
        public bool correctFalling = false;
        public bool perfectFalling = false;
        public Transform newBlock = null;
        public float distanceToLastBlock = 0;

        public override void SetParameters(params object[] parameters) 
        {
            correctFalling = (bool)parameters[0];
            perfectFalling = (bool)parameters[1];
            newBlock = (Transform)parameters[2];
            distanceToLastBlock = (float)parameters[3];
        }
    }
}