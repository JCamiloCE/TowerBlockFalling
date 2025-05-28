using Emc2.Scripts.GameplayEvents;
using JCC.Utils.GameplayEventSystem;
using UnityEngine;

namespace Emc2.Scripts.Effects
{
    public class EffectImplPerfectStartsFit : MonoBehaviour, IEffect, IEventListener<PerfectTimeEvent>
    {
        [SerializeField] private ParticleSystem _particles = null;

        #region IEventListener
        public void OnEvent(PerfectTimeEvent event_data)
        {
            if (event_data.isStart)
                StartEffect();
            else
                StopEffect();
        }
        #endregion IEventListener

        #region IEffect
        public void StartEffect()
        {
            _particles.Play();
        }

        public void StopEffect() 
        {
            _particles.Stop();
        }
        #endregion

        #region public
        public void RegisterEvent() 
        {
            EventManager.AddListener(this);
        }

        public void UnregisterEvent()
        {
            EventManager.RemoveListener(this);
        }
        #endregion public
    }
}