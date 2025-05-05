using UnityEngine;

namespace JCC.Utils.DebugManager
{
    public class DebugUnityImpl : IDebug
    {
        void IDebug.LogError(string message)
        {
            Debug.LogError(message);
        }

        void IDebug.LogVerbose(string message)
        {
            Debug.Log(message);
        }

        void IDebug.LogWarning(string message)
        {
            Debug.LogWarning(message);
        }
    }
}