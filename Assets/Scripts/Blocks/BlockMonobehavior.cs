using JCC.Utils.Pool;
using UnityEngine;

namespace Emc2.Scripts.Blocks
{
    public class BlockMonobehavior : MonoBehaviour, IPoolResettable
    {
        [SerializeField] private Rigidbody _rg = null;

        #region IPoolResettable
        public void ResetPoolObject()
        {
            _rg.linearVelocity = Vector3.zero;      
            _rg.angularVelocity = Vector3.zero;
            _rg.Sleep();
            _rg.constraints = RigidbodyConstraints.FreezeAll;
            _rg.useGravity = true;

            transform.rotation = Quaternion.identity;
        }
        #endregion IPoolResettable
    }
}