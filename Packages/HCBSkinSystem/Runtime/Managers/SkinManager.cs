using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;


namespace HCB.SkinSystem
{
    public class SkinManager : Singleton<SkinManager>
    {
        public SkinDatabase SkinDatabase;

        [HideInInspector]
        public UnityEvent<SkinItemData> OnSkinActivated = new UnityEvent<SkinItemData>();
        [HideInInspector]
        public UnityEvent OnNewSkinBought = new UnityEvent();


        private PlayerSkinData playerSkinData;
        protected PlayerSkinData PlayerSkinData
        {
            get
            {
                return (playerSkinData == null) ? playerSkinData = SaveLoadManager.LoadPDP<PlayerSkinData>(SavedFileNameHolder.SkinData, new PlayerSkinData()) : playerSkinData;
            }
        }

        public SkinItemData GetCurrentSkin(SkinType skinType)
        {
            string id = PlayerSkinData.GetCurrentSkin(skinType);

            return SkinDatabase.GetSkinById(id, skinType);
        }

        public SkinItemData GetSkinByID(string skinID)
        {
            return SkinDatabase.GetSkinById(skinID);
        }

        public bool IsSkinUnlocked(string id)
        {
            return PlayerSkinData.IsSkinUnlocked(id);
        }

        public void AddSkinToPlayerData(SkinItemData skin)
        {
            //add new skin to bought skin data
            PlayerSkinData.UnlockSkin(skin.SkinId);
        }

        public SkinItemData GetRandomSkin()
        {
            return SkinDatabase.GetRandomSkin(PlayerSkinData);
        }

        public SkinItemData GetRandomSkin(SkinType skinType)
        {
            return SkinDatabase.GetRandomSkin(PlayerSkinData, skinType);
        }

        public SkinItemData GetRandomSkin(SkinType skinType, SkinRarity skinRarity)
        {
            return SkinDatabase.GetRandomSkin(PlayerSkinData, skinRarity, skinType);
        }

        public bool CheckSkinAvialibity(SkinType skinType, SkinRarity skinRarity)
        {
            SkinItemData skinItemData = SkinDatabase.GetRandomSkin(PlayerSkinData, skinRarity, skinType);
            if (skinItemData == null)
                return false;
            else return true;
        }

        [Button]
        public void BuySkin(SkinItemData skin)
        {
            AddSkinToPlayerData(skin);
            OnNewSkinBought.Invoke();
            EquipSkin(skin);
            EventManager.OnLogEvent.Invoke("SkinEvent", "SkinBougt", skin.SkinId);
        }

        [Button]
        public void EquipSkin(SkinItemData skin)
        {
            //if skin is not already equipped, equip it
            string currentSkinId = PlayerSkinData.GetCurrentSkin(skin.SkinType);
            if (string.Equals(currentSkinId, skin.SkinId))
                return;

            if (!PlayerSkinData.IsSkinUnlocked(skin.SkinId))
                return;

            PlayerSkinData.SetCurrentSkin(skin.SkinId, skin.SkinType);
            OnSkinActivated.Invoke(skin);
            EventManager.OnLogEvent.Invoke("SkinEvent", "EquipSkin", skin.SkinId);

        }

        public List<SkinItemData> GetSkinsByType(SkinType skinType)
        {
            return SkinDatabase.GetSkinsByType(skinType);
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveLoadManager.SavePDP(PlayerSkinData, SavedFileNameHolder.SkinData);
            }
        }

        private void OnApplicationQuit()
        {
            SaveLoadManager.SavePDP(PlayerSkinData, SavedFileNameHolder.SkinData);
        }
    }
}
