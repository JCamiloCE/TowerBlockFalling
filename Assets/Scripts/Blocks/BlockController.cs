using JCC.Utils.Pool;
using UnityEngine;

public class BlockController : MonoBehaviour, IPoolResettable
{
    #region Pool
    void IPoolResettable.ResetPoolObject()
    {
        throw new System.NotImplementedException();
    }
    #endregion Pool
}
