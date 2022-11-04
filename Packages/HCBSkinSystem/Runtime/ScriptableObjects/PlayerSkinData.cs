using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.SkinSystem
{
    [System.Serializable]
    public class PlayerSkinData 
    {
        [SerializeField]
        private List<string> UnlockedSkins = new List<string>();

        [SerializeField]
        private Dictionary<SkinType, string> CurrentSkinCollection = new Dictionary<SkinType, string>();

        public PlayerSkinData()
        {
            UnlockedSkins.Add("None");
            CurrentSkinCollection[SkinType.Fullbody] = "None";
        }

        public bool IsSkinUnlocked (string id)
        {
            return UnlockedSkins.Contains(id);
        }

        public string GetCurrentSkin(SkinType skinType)
        {
            if (!CurrentSkinCollection.ContainsKey(skinType))
                return CurrentSkinCollection[skinType] = "None";

            return CurrentSkinCollection[skinType];
        }

        public void SetCurrentSkin(string id, SkinType skinType)
        {
            CurrentSkinCollection[skinType] = id;
        }

        public void UnlockSkin(string id)
        {
            if (!UnlockedSkins.Contains(id))
                UnlockedSkins.Add(id);
        }
    }
}
