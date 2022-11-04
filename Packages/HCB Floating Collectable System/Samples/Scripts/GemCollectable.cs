using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HCB.Core;

namespace HCB.FloatingCollectableSystem.Examples
{
    public class GemCollectable : FloatingCollectableBase
    {
        public ExchangeType ExchangeType;
        public int Amount;

        #region Constraints
        private const float OFFSET_MIN = 0.1f;
        private const float OFFSET_MAX = 0.25f;

        private const float MOVE_DURATION_MIN = 0.5f;
        private const float MOVE_DURATION_MAX = 0.6f;

        private const float ROTATION_DURATION_MIN = 3f;
        private const float ROTATION_DURATION_MAX = 5f;
        #endregion

        private void Start()
        {
            float moveOffset = Random.Range(OFFSET_MIN, OFFSET_MAX);
            float moveDuration = Random.Range(MOVE_DURATION_MIN, MOVE_DURATION_MAX);
            float rotationDuration = Random.Range(ROTATION_DURATION_MIN, ROTATION_DURATION_MAX);

            transform.DOMoveY(transform.position.y + moveOffset, moveDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            transform.DORotate(Vector3.up * 90f, rotationDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        }

        //Only for Example Level.
        public override void OnFloatingMovementCompleted()
        {
            EventManager.OnCurrencyInteracted.Invoke(ExchangeType, Amount);
        }
    }
}
