using JCC.Utils.Pool;
using JCC.Utils.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Emc2.Scripts.BuildingTenant
{
    public class TenantNpc : MonoBehaviour, IPoolResettable
    {
        [SerializeField] private SpriteRenderer _body;
        [SerializeField] private List<Sprite> _spritesBody;
        [SerializeField] private SpriteRenderer _balloon;
        [SerializeField] private List<Sprite> _spritesBalloon;
        [SerializeField] private float _speed = 2f;

        private IRandom _random = null;
        private Coroutine _moveCoroutine = null;
        private Action _endCb = null;

        #region IPoolResettable
        public void ResetPoolObject()
        {
            //NOOP
        }
        #endregion

        #region public
        public void StartMovement(Vector3 startPoint, Transform endPoint, Action endCb) 
        {
            _endCb = endCb;
            ChangeSprites(startPoint, endPoint);
            StopMovement();
            _moveCoroutine = StartCoroutine(MoveNpc(startPoint, endPoint));
        }
        #endregion public

        #region private
        private void ChangeSprites(Vector3 startPoint, Transform endPoint) 
        {
            if (_random == null)
            {
                _random = new RandomUnity();
            }
            _body.sprite = _spritesBody[_random.GetRandomIndexInList(_spritesBody)];
            _balloon.sprite = _spritesBalloon[_random.GetRandomIndexInList(_spritesBalloon)];
            int sing = (startPoint.x > endPoint.position.x) ? -1 : 1;
            transform.localScale = new Vector3(1f * sing, 1f, 1f);
        }

        private void StopMovement()
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
            _moveCoroutine = null;
        }

        private IEnumerator MoveNpc(Vector3 startPoint, Transform endPoint) 
        {
            transform.position = startPoint;
            float distance = Vector3.Distance(startPoint, endPoint.position);
            float finishTime = distance / _speed;
            float timeElapsed = 0f;

            while (timeElapsed < finishTime)
            {
                transform.position = Vector3.Lerp(startPoint, endPoint.position, timeElapsed / finishTime);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPoint.position;
            _moveCoroutine = null;
            _endCb?.Invoke();
        }
        #endregion
    }
}