using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 
using HCB.Core;
using HCB.Utilities;
using System.Linq;

namespace HCB.SkinSystem
{
    public class SkinDatabase : ScriptableObject
    {
        public List<SkinItemData> SkinItemDatas = new List<SkinItemData>();

        public List<SkinItemData> GetSkinsByType(SkinType skinType)
        {
            return new List<SkinItemData>(SkinItemDatas.Where(x => x.SkinType == skinType));
        }

        public SkinItemData GetSkinById(string id)
        {
            return SkinItemDatas
                .Where(x => x.SkinId == id).First();
        }

        public SkinItemData GetSkinById(string id, SkinType skinType)
        {
            return SkinItemDatas
                .Where(x => x.SkinId == id)
                .Where(x => x.SkinType == skinType).First();
        }

        public SkinItemData GetRandomSkin(PlayerSkinData playerSkinData)
        {
            List<SkinItemData> shuffledSkins = new List<SkinItemData>(SkinItemDatas);
            shuffledSkins.Shuffle();

            return shuffledSkins
                 .Where(x => playerSkinData.IsSkinUnlocked(x.SkinId) == false)
                 .FirstOrDefault();
        }

        public SkinItemData GetRandomSkin(PlayerSkinData playerSkinData, SkinType skinType)
        {
            List<SkinItemData> shuffledSkins = new List<SkinItemData>(SkinItemDatas);
            shuffledSkins.Shuffle();

            return shuffledSkins
                 .Where(x => x.SkinType == skinType).FirstOrDefault();
        }

        public SkinItemData GetRandomSkin(PlayerSkinData playerSkinData, SkinRarity skinRarity, SkinType skinType)
        {
            List<SkinItemData> shuffledSkins = new List<SkinItemData>(SkinItemDatas);
            shuffledSkins.Shuffle();

            return shuffledSkins
                 .Where(x => playerSkinData.IsSkinUnlocked(x.SkinId) == false)
                 .Where(x => x.SkinRarity == skinRarity)
                 .Where(x => x.SkinType == skinType).FirstOrDefault();
        }
    }
}
