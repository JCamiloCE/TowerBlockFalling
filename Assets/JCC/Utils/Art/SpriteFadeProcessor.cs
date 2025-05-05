using JCC.Utils.LifeCycle;
using System.Collections;
using UnityEngine;

namespace JCC.Utils.Art
{
    public class SpriteFadeProcessor : MonoBehaviour, ILifeCycle
    {
        private bool _wasInitialized = false;
        private SpriteRenderer _spriteRenderer = null;
        private float _currentFadeDuration = 0;
        private Coroutine _currentCoroutine = null;

        public bool WasInitialized() => _wasInitialized;
        public bool IsProcessFade() => _currentCoroutine != null;

        public bool Initialization(params object[] parameters)
        {
            _spriteRenderer = parameters[0] as SpriteRenderer;

            _wasInitialized = true;
            return true;
        }

        public void SetAlpha(float newAlpha) 
        {
            Color originalColor = _spriteRenderer.color;
            originalColor.a = newAlpha;
            _spriteRenderer.color = originalColor;
        }

        public void StartFadeIn(float fadeDuration) 
        {
            _currentFadeDuration = fadeDuration;
            StopCurrentCoroutine();
            _currentCoroutine = StartCoroutine(FadeIn());
        }

        public void StartFadeOut(float fadeDuration)
        {
            _currentFadeDuration = fadeDuration;
            StopCurrentCoroutine();
            _currentCoroutine = StartCoroutine(FadeOut());
        }

        private void StopCurrentCoroutine() 
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }

        private IEnumerator FadeIn() 
        {
            Color originalColor = _spriteRenderer.color;
            float timeElapsed = 0f;

            while (timeElapsed < _currentFadeDuration)
            {
                float alpha = Mathf.Lerp(0f, 1f, timeElapsed / _currentFadeDuration);
                _spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            SetAlpha(1f);
            StopCurrentCoroutine();
        }

        private IEnumerator FadeOut()
        {
            Color originalColor = _spriteRenderer.color;
            float timeElapsed = 0f;

            while (timeElapsed < _currentFadeDuration)
            {
                float alpha = Mathf.Lerp(1f, 0f, timeElapsed / _currentFadeDuration);
                _spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            SetAlpha(0f);
            StopCurrentCoroutine();
        }
    }
}
