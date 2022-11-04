using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace HCB.AchivementSystem
{
    [System.Serializable]
    public class PlayerAchivementData
    {
        public List<AchivementModel> Achivements = new List<AchivementModel>();

        public void Initialize(List<AchivementModel> achivements)
        {
            for (int i = 0; i < achivements.Count; i++)
            {
                if (!Achivements.Any(x => x.AchivementID == achivements[i].AchivementID && x.UnlockTreashold == achivements[i].UnlockTreashold))
                    Achivements.Add(achivements[i]);
            }
        }

        public AchivementModel UpdateAchivement(AchivementModel achivement)
        {
            if (!Achivements.Any(x=>x.AchivementID==achivement.AchivementID && x.UnlockTreashold == achivement.UnlockTreashold && x.AchivementUnlockCondition== achivement.AchivementUnlockCondition))
            {
                AddAchivement(achivement);
                return achivement;
            }

            AchivementModel currentAchivement = Achivements
                .Where(x => x.AchivementID == achivement.AchivementID)
                .Where(x => x.UnlockTreashold == achivement.UnlockTreashold)
                .Where(x => x.AchivementUnlockCondition == achivement.AchivementUnlockCondition)
                .First();

            currentAchivement = achivement;
            return achivement;
        }

        public AchivementModel GetAchivement(string achivementID)
        {

            bool exist = Achivements.Any(x => x.AchivementID == achivementID);
            if (exist)
            {
                return Achivements
                  .Where(x => x.AchivementID == achivementID)
                  .Where(x => x.IsComplete == false)
                  .FirstOrDefault();
            }
            else return null;

        }

        public AchivementModel GetAchivementNotCollected(string achivementID)
        {

            bool exist = Achivements.Any(x => x.AchivementID == achivementID);
            if (exist)
            {
                return Achivements
                  .Where(x => x.AchivementID == achivementID)
                  .Where(x => x.IsCollected == false)
                  .FirstOrDefault();
            }
            else return null;

        }

        public AchivementModel GetAchivement(AchivementModel achivementModel)
        {
            for (int i = 0; i < Achivements.Count; i++)
            {
                if (IsSameAchivement(Achivements[i], achivementModel))
                    return Achivements[i];
            }
            return achivementModel;
        }

        private bool IsSameAchivement(AchivementModel achivementA, AchivementModel achivementB)
        {
            if (string.Equals(achivementA.AchivementID, achivementB.AchivementID) && achivementA.AchivementUnlockCondition == achivementB.AchivementUnlockCondition && achivementA.UnlockTreashold == achivementB.UnlockTreashold)
                return true;

            return false;
        }

        private void AddAchivement(AchivementModel achivement)
        {
            Achivements.Add(achivement);
        }

        
    }
}
