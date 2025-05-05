using JCC.Utils.DebugManager;
using JCC.Utils.LifeCycle;
using UnityEngine;

public class BlockMovement : MonoBehaviour, ILifeCycle
{
    private IMovement _movement;
    private bool _wasInitialized;

    #region ILifeCycle
    public bool WasInitialized() => _wasInitialized;

    public bool Initialization(params object[] parameters)
    {
        _wasInitialized = true;
        _wasInitialized = _wasInitialized && SetMovement(parameters[0] as IMovement);
        return _wasInitialized;
    }
    #endregion ILifeCycle

    #region public
    #endregion public

    #region internal
    internal bool SetMovement(IMovement movement) 
    {
        if (movement == null)
        {
            DebugManager.LogError("BlockMovement.SetMovement :: movement is null");
            return false;
        }
        _movement = movement;
        return true;
    }
    #endregion internal

    #region public
    #endregion public
}
