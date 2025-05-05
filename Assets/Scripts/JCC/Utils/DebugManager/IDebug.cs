using UnityEngine;

namespace JCC.Utils.DebugManager
{
    public interface IDebug
    {
        public void LogVerbose(string message);
        public void LogWarning(string message);
        public void LogError(string message);
    }
}