using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using System;
using DG.Tweening;
using HCB.PoolingSystem;
using HCB.Utilities;

namespace HCB.FloatingCollectableSystem
{
    public class FloatingCollectablePanel : MonoBehaviour
    {
        public static FloatingCollectablePanel Instance;

        private Canvas _canvas;
        private Canvas Canvas => _canvas == null ? _canvas = GetComponentInParent<Canvas>() : _canvas;
     
        public Transform CollectableImageMoveTarget;
        public Transform MoneyImage;
        
        private const float Collectable_IMAGE_MOVEMENT_DURATION = 0.6f;
        private const string IMAGE_POOL_ID = "FloatingMoneyImage";
        private const float PUNCH_DURATION = 0.2f;
        private const float PUNCH_STRENGTH = 0.65f;

        private string _punchTweenID;

        private void Awake()
        {
            Instance = this;
            _punchTweenID = GetInstanceID() + "PunchTweenID";
        }

        private void OnEnable()
        {
            FloatingCollectableEventManager.OnFloatingCollectableCollected.AddListener(OnCollected);
        }

        private void OnDisable()
        {
            FloatingCollectableEventManager.OnFloatingCollectableCollected.RemoveListener(OnCollected);
        }

        public void CreateFloatingMoney(Vector3 worldPosition, float duration, float delay, Action onComplete) 
        {
            Vector3 collectableUIPosition = HCBUtilities.WorldToUISpace(Canvas, worldPosition);
            GameObject collectableImage = CreateCollectableImage(collectableUIPosition);

            collectableImage.transform.DOMove(CollectableImageMoveTarget.transform.position, duration).SetDelay(delay).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                collectableImage.SetActive(false);
                PunchMoneyImage();
                onComplete?.Invoke();
            });
        }

        private void OnCollected(Vector3 worldPosition, Action onComplete)
        {
            CreateFloatingMoney(worldPosition, Collectable_IMAGE_MOVEMENT_DURATION, 0f, onComplete);
        }
               
        private GameObject CreateCollectableImage(Vector3 position) 
        {           
            GameObject collectableImage = PoolingSystem.PoolingSystem.Instance.InstantiateAPS(IMAGE_POOL_ID, position);
            collectableImage.transform.SetParent(CollectableImageMoveTarget.transform);
            return collectableImage;
        }

        private void PunchMoneyImage()
        {
            DOTween.Complete(_punchTweenID);
            MoneyImage.DOPunchScale(Vector3.one * PUNCH_STRENGTH, PUNCH_DURATION).SetEase(Ease.Linear).SetId(_punchTweenID);
        }        
    }
}
