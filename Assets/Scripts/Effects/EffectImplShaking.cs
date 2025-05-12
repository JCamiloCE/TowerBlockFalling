using JCC.Utils.GameplayEventSystem;
using JCC.Utils.Random;
using Scripts.GameplayEvents;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Effects
{
    public class EffectImplShaking : MonoBehaviour, IEffect, IEventListener<StartShakeEffectEvent>
    {
        [SerializeField] private Transform _shakeObj = null;
        [SerializeField] private float _minXShake = 2f;
        [SerializeField] private float _maxXShake = 2f;
        [SerializeField] private float _minYShake = 2f;
        [SerializeField] private float _maxYShake = 2f;
        [SerializeField] private float _shakeTime = 2f;

        private Coroutine _shake;
        private IRandom _random;

        #region IEffect
        public void StartEffect()
        {
            StopShakingEffect();
            _shake = StartCoroutine(Shake());
        }
        #endregion

        #region IEventListener
        public void OnEvent(StartShakeEffectEvent event_data)
        {
            StartEffect();
        }
        #endregion

        #region private
        private void Start()
        {
            EventManager.AddListener(this);
            _random = new RandomUnity();
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(this);
        }

        private void StopShakingEffect()
        {
            if (_shake != null)
            {
                StopCoroutine(_shake);
            }
            _shake = null;
        }

        private IEnumerator Shake() 
        {
            Vector3 initialPos = _shakeObj.position;
            float currentTime = 0f;
            while (currentTime < _shakeTime)
            {
                float x = _random.GetRandomFloatBetween(_minXShake, _maxXShake);
                float y = _random.GetRandomFloatBetween(_minYShake, _maxYShake);
                _shakeObj.position = initialPos + new Vector3(x, y, 0f);
                currentTime += Time.deltaTime;
                yield return null;
            }
            _shakeObj.position = initialPos;
        }
        #endregion
    }
}