using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using System;
using DG.Tweening;

namespace HCB.FloatingCollectableSystem
{
    public class FloatingCollectablePanel : MonoBehaviour
    {
        Canvas _canvas;
        Canvas Canvas => _canvas == null ? _canvas = GetComponentInParent<Canvas>() : _canvas;

        public GameObject CollectableImagePrefab;
        public Transform CollectableImageMoveTarget;
        
        private const float Collectable_IMAGE_MOVEMENT_DURATION = 0.6f;

        private void OnEnable()
        {
            EventManager.OnFloatingCollectableCollected.AddListener(OnCollected);
        }

        private void OnDisable()
        {
            EventManager.OnFloatingCollectableCollected.RemoveListener(OnCollected);
        }

        private void OnCollected(Vector3 gemPosition, Action onCompleted)
        {
            Vector3 collectableUIPosition = WorldToUISpace(Canvas, gemPosition);
            GameObject collectableImage = CreateCollectableImage(collectableUIPosition);

            collectableImage.transform.DOMove(CollectableImageMoveTarget.transform.position, Collectable_IMAGE_MOVEMENT_DURATION).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(collectableImage);
                onCompleted();
            });            
        }

        //Normally this should be create by Pooling System
        private GameObject CreateCollectableImage(Vector3 position) 
        {
            GameObject collectableImage = Instantiate(CollectableImagePrefab, position, CollectableImagePrefab.transform.rotation);
            collectableImage.transform.SetParent(CollectableImageMoveTarget.transform);
            return collectableImage;
        }

        private Vector3 WorldToUISpace(Canvas canvas, Vector3 worldPosition)
        {
            //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

            //Convert the screenpoint to ui rectangle local point
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out Vector2 localPoint);

            //Convert the local point to world point
            return canvas.transform.TransformPoint(localPoint);
        }
    }
}
