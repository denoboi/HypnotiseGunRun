using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HCB.Core;
using HCB.Utilities;
using System.Linq;
using Sirenix.OdinInspector;

namespace HCB.AchivementSystem
{
    public class AchivementManager : Singleton<AchivementManager>
    {
        [SerializeField]
        private List<AchivementData> AchivementData;
        public AchivementEvent OnAchivementUnlock = new AchivementEvent();

        private PlayerAchivementData playerAchivementData;
        public PlayerAchivementData PlayerAchivementData { get { return (playerAchivementData == null) ? playerAchivementData = SaveLoadManager.LoadPDP<PlayerAchivementData>(SavedFileNameHolder.AchivementData, new PlayerAchivementData()) : playerAchivementData; } }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            List<AchivementModel> achivementModels = new List<AchivementModel>();
            for (int i = 0; i < AchivementData.Count; i++)
            {
                achivementModels.AddRange(AchivementData[i].Achivements);
            }

            PlayerAchivementData.Initialize(achivementModels);
        }

        public AchivementModel UpdateAchivement(AchivementModel achivementModel)
        {
            
            AchivementModel model = PlayerAchivementData.UpdateAchivement(achivementModel);
            return model;
        }

        public AchivementModel GetAchivement(string achivementID)
        {
            try
            {
                return PlayerAchivementData.GetAchivement(achivementID);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(achivementID + " Does not exist Error: " + ex);
            }
        }

        public AchivementModel GetAchivementNotCollected(string achivementID)
        {
            try
            {
                return PlayerAchivementData.GetAchivementNotCollected(achivementID);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(achivementID + " Does not exist Error: " + ex);
            }
        }


        [Button]
        private void UnlockAchivement(string achivementID)
        {
            AchivementModel achivement = GetAchivement(achivementID);
            switch (achivement.AchivementUnlockCondition)
            {
                case AchivementUnlockCondition.Grather:
                    achivement.CompleteAchivement(achivement.UnlockTreashold + 1);
                    break;
                case AchivementUnlockCondition.Equal:
                    achivement.CompleteAchivement(achivement.UnlockTreashold);
                    break;
                case AchivementUnlockCondition.Trigger:
                    achivement.CompleteAchivement(0);
                    break;
                default:
                    break;
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveLoadManager.SavePDP(PlayerAchivementData, SavedFileNameHolder.AchivementData);
            }
        }

        private void OnApplicationQuit()
        {
            SaveLoadManager.SavePDP(PlayerAchivementData, SavedFileNameHolder.AchivementData);
        }
    }


    public class AchivementEvent : UnityEvent<AchivementModel> { }
}
