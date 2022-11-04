using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HCB.AchivementSystem
{
    public enum AchivementRewardType { Currency, Skin }
    public enum AchivementUnlockCondition { Grather, Equal, Trigger }

    [System.Serializable]
    public class AchivementModel
    {
        [FoldoutGroup("SetUp")]
        public string AchivementID;
        [FoldoutGroup("SetUp")]
        [TextArea]
        public string AchivementDescription;

        [FoldoutGroup("Reward")]
        public AchivementRewardType RewardType = AchivementRewardType.Currency;

        #region Currency Reward
        [ShowIf("isCurrencyReward")]
        [FoldoutGroup("Reward")]
        public ExchangeType ExchangeType = ExchangeType.Coin;

        [FoldoutGroup("Reward")]
        [ShowIf("isCurrencyReward")]
        [ValueDropdown("currencyRewardAmounts")]
        public float CurrencyRewardAmount = 50;
        #endregion

        #region Skin Reward
        [FoldoutGroup("Reward")]
        [HideIf("isCurrencyReward")]
        public string SkinID;
        #endregion

        #region Unlock Condition
        [FoldoutGroup("Unlock Settings")]
        [HideIf("isTriggerAchivement")]
        public float UnlockTreashold = 1;

        [FoldoutGroup("Unlock Settings")]
        public AchivementUnlockCondition AchivementUnlockCondition = AchivementUnlockCondition.Grather;
      

        [SerializeField]
        [ReadOnly]
        [HideIf("isTriggerAchivement")]
        public float currentValue;
        #endregion

        #region Information
        public bool IsComplete;
        public bool IsCollected;
        #endregion



        private bool isCurrencyReward { get { return RewardType == AchivementRewardType.Currency; } }
        private bool isTriggerAchivement { get { return AchivementUnlockCondition == AchivementUnlockCondition.Trigger; } }

        private float[] currencyRewardAmounts = { 50, 100, 250, 500, 1000, 2500, 5000, 10000 };

        public bool CompleteAchivement(float _currentValue)
        {

            currentValue = _currentValue;

            if (IsComplete)
                return IsComplete;

            switch (AchivementUnlockCondition)
            {
                case AchivementUnlockCondition.Grather:
                    IsComplete = _currentValue > UnlockTreashold;
                    if (IsComplete)
                        AchivementManager.Instance.OnAchivementUnlock.Invoke(this);

                    Debug.Log("Achivement " + AchivementID + " isComplete" + IsComplete);
                    return IsComplete;
                case AchivementUnlockCondition.Equal:
                    IsComplete = _currentValue == UnlockTreashold;
                    if (IsComplete)
                        AchivementManager.Instance.OnAchivementUnlock.Invoke(this);

                    Debug.Log("Achivement " + AchivementID + " isComplete" + IsComplete);
                    return IsComplete;
                case AchivementUnlockCondition.Trigger:
                     IsComplete = true;
                    if (IsComplete)
                        AchivementManager.Instance.OnAchivementUnlock.Invoke(this);

                    Debug.Log("Achivement " + AchivementID + " isComplete" + IsComplete);
                    return IsComplete;
                default:
                    return IsComplete = true;
            }
        }

        //public AchivementModel Clone()
        //{
        //    return (AchivementModel)this.MemberwiseClone();
        //}

        public void ResetAchivement()
        {
            IsComplete = false;
            currentValue = 0;
        }
    }
}
