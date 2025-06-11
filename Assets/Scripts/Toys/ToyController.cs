using log4net.Util;
using System;
using System.Collections;
using UnityEngine;

namespace Emc2.Scripts.BuildingTenant
{
    public class ToyController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRender = null;

        private Coroutine _moveCoroutine = null;
        private Action _cbFinishMovement = null;

        public void InitMovement(ToyData toyData, Vector3 initPos, Vector3 endPos, Action cbFinishMovement) 
        {
            StopMovementCoroutine();
            _spriteRender.sprite = toyData.sprite;
            _cbFinishMovement = cbFinishMovement;
            _moveCoroutine = StartCoroutine(StartMovement(initPos, endPos, toyData.speed));
        }

        private void StopMovementCoroutine() 
        {
            if (_moveCoroutine != null) 
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }
        }

        private IEnumerator StartMovement(Vector3 initPos, Vector3 endPos, float moveSpeed) 
        {
            transform.position = initPos;
            Vector3 startPosition = transform.position;
            float distance = Vector3.Distance(startPosition, endPos);
            float finishTime = distance / moveSpeed;
            float timeElapsed = 0f;

            while (timeElapsed < finishTime)
            {
                transform.position = Vector3.Lerp(startPosition, endPos, timeElapsed / finishTime);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _cbFinishMovement?.Invoke();
            _moveCoroutine = null;
        }
    }
}