using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HCB.AchivementSystem
{
    public class AchivementData : ScriptableObject
    {
        
        public List<AchivementModel> Achivements = new List<AchivementModel>();


        public AchivementModel GetAchivement(string achivementID)
        {
            return Achivements
                .Where(x => x.AchivementID == achivementID)
                .Where(x => x.IsComplete == false).First();
        }

        public AchivementModel GetAchivementNotCollected(string achivementID)
        {
            return Achivements
                .Where(x => x.AchivementID == achivementID)
                .Where(x => x.IsCollected == false).First();
        }

        [Button]
        private void ResetData()
        {
            foreach (var item in Achivements)
            {
                item.ResetAchivement();
            }
        }
    }
}
