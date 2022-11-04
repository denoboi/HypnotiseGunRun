using System.Collections.Generic;
using System.Collections;
using UnityEngine;
#if UNITY_ANDROID
using Google.Play.Review;
#endif
using System.Linq;
using HCB.Core;
using HCB.Utilities;
using Sirenix.OdinInspector;


namespace HCB.Core
{
    [System.Serializable]
    public class RateUsLevelHolder
    {
        public int[] targetLevels = { 4, 10, 15 };

        [Button]
        private void ExportAsJson()
        {
            string json = JsonUtility.ToJson(this);
            Debug.Log(json);
        }
    }

    public class RateUsManager : Singleton<RateUsManager>
    {
        public RateUsLevelHolder RateUsLevelHolder = new RateUsLevelHolder();

        private void OnEnable()
        {
            SceneController.Instance.OnSceneUnloading.AddListener(Rate);
        }

        private void OnDisable()
        {
            SceneController.Instance.OnSceneUnloading.AddListener(Rate);
        }

        public void Rate()
        {
            int fakeLevel = PlayerPrefs.GetInt(PlayerPrefKeys.FakeLevel, 1);
            if (!RateUsLevelHolder.targetLevels.Contains(fakeLevel))
                return;

            Debug.Log("Review Shown");

#if UNITY_IOS
            UnityEngine.iOS.Device.RequestStoreReview();
#elif UNITY_ANDROID
        StartCoroutine(RequestReviewAndroidCo());
#endif
        }

#if UNITY_ANDROID

        IEnumerator RequestReviewAndroidCo()
        {
            ReviewManager reviewManager = new ReviewManager();
            var requestFlowOperation = reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;
            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // Log error. For example, using requestFlowOperation.Error.ToString().
                EventManager.OnLogEvent.Invoke("App Review", "ReviewRequestError", requestFlowOperation.Error.ToString());
                yield break;
            }
            PlayReviewInfo _playReviewInfo = requestFlowOperation.GetResult();

            var launchFlowOperation = reviewManager.LaunchReviewFlow(_playReviewInfo);
            yield return launchFlowOperation;
            _playReviewInfo = null; // Reset the object
            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // Log error. For example, using requestFlowOperation.Error.ToString().
                EventManager.OnLogEvent.Invoke("App Review", "ReviewLaunchError", launchFlowOperation.Error.ToString());

                yield break;
            }
            // The flow has finished. The API does not indicate whether the user
            // reviewed or not, or even whether the review dialog was shown. Thus, no
            // matter the result, we continue our app flow.
        }
#endif

    }
}
