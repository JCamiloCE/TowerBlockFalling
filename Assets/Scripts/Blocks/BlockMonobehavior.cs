using Emc2.Scripts.Effects;
using JCC.Utils.Pool;
using UnityEngine;

namespace Emc2.Scripts.Blocks
{
    public class BlockMonobehavior : MonoBehaviour, IPoolResettable
    {
        [SerializeField] private Rigidbody _rg = null;
        [SerializeField] private EffectImplPerfectStartsFit _effectPerfect = null;

        #region IPoolResettable
        public void ResetPoolObject()
        {
            _rg.linearVelocity = Vector3.zero;      
            _rg.angularVelocity = Vector3.zero;
            _rg.Sleep();
            _rg.constraints = RigidbodyConstraints.FreezeAll;
            _rg.useGravity = true;

            transform.rotation = Quaternion.identity;
            _effectPerfect.UnregisterEvent();
        }
        #endregion IPoolResettable

        #region public
        public void ForcePerfectEffect() 
        {
            _effectPerfect.StartEffect();
        }

        public void RegisterEventPerfect() 
        {
            _effectPerfect.RegisterEvent();
        }
        #endregion public

        #region
        private void OnDestroy()
        {
            _effectPerfect?.UnregisterEvent();
        }
        #endregion
    }
}