using Emc2.Scripts.GameplayEvents;
using JCC.Utils.GameplayEventSystem;
using UnityEngine;

namespace Emc2.Scripts.GeneralManagers
{
    public class LifeManager : MonoBehaviour, IEventListener<FinishFallingBlockEvent>
    {
        [SerializeField] private int _initialAmountLife;

        private int _currentLife = 0;

        #region IEventListener
        public void OnEvent(FinishFallingBlockEvent event_data)
        {
            if (!event_data.correctFalling)
            {
                ReduceLife();
            }
        }
        #endregion

        #region private
        private void Start()
        {
            _currentLife = _initialAmountLife;
            EventManager.AddListener(this);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(this);
        }

        private void ReduceLife() 
        {
            _currentLife--;
            if (_currentLife <= 0) 
            {
                EventManager.TriggerEvent<LoseGameEvent>();
            }
        }
        #endregion private
    }
}